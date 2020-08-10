using System;
using System.Collections.Generic;
using System.Linq;
using FamilyTree.Core.Interfaces;
using FamilyTree.Core.Enums;
using FamilyTree.Core.Exceptions;

namespace FamilyTree.Core.DataStructures
{
    ///<summary>
    /// This class represents the entire family tree of individuals along with their relationships
    ///</summary>
    public class FamilyTreeGraph : IFamilyTreeGraph
    {
        private Dictionary<string, FamilyTreeNode> map;

        public FamilyTreeGraph()
        {
            map = new Dictionary<string, FamilyTreeNode>();
        }
        
        ///<summary>
        /// Adds the provided husband and wife to the Family Tree.
        ///</summary>
        public void AddSpouse(string husbandName, string wifeName)
        {
            FamilyTreeNode husband = null;
            FamilyTreeNode wife = null;
            
            if(map.ContainsKey(husbandName))
                husband = map[husbandName];
            
            if(map.ContainsKey(wifeName))
                wife = map[wifeName];
                
            if(husband == null)
                husband = new FamilyTreeNode(husbandName, Gender.Male);
                
            if(wife == null)
                wife = new FamilyTreeNode(wifeName, Gender.Female);
                
            // Validates based on the genders of husband/wife.
            if(husband.Gender == Gender.Male && wife.Gender == Gender.Female)
            {
                // Already a spouse. Nothing else to do. Return.
                if(husband.Spouse != null && husband.Spouse.Name == wifeName && wife.Spouse != null && wife.Spouse.Name == husbandName)
                    return;
                // Both dont have spouses, can be added as a spouse.
                else if(husband.Spouse == null && wife.Spouse == null)
                {
                    husband.Spouse = wife;
                    wife.Spouse = husband;
                    husband.Children = wife.Children;
                }
                // Already has a different spouse.
                else
                    throw new SpouseAdditionFailedException();           
            }
            else
                throw new SpouseAdditionFailedException();
            
            if(!map.ContainsKey(husbandName)) map[husbandName] = husband;
            if(!map.ContainsKey(wifeName)) map[wifeName] = wife;
                
        }
        
        ///<summary>
        /// Adds the child for the provided mother into the family tree.
        ///</summary>
        public void AddChild(string motherName, string childName, Gender childGender)
        {   
            if(!map.ContainsKey(motherName))
                throw new PersonNotFoundException();            
                
            FamilyTreeNode mother = map[motherName];
            FamilyTreeNode child = null;
            
            if(!map.ContainsKey(childName))
                child = new FamilyTreeNode(childName, childGender);
            else
                child = map[childName];
            
            // Validates the child and mother's genders, and checks if the child already has a different mother.
            if(child.Gender != childGender || mother.Gender != Gender.Female || (child.Mother != null && child.Mother != mother))
                throw new ChildAdditionFailedException();
            
            if(child.Mother == null)
            {
                child.Mother = mother;
                child.Father = mother.Spouse;
                mother.Children.Add(child);
            }

            if(!map.ContainsKey(childName)) map[childName] = child;           
        }
        
        ///<summary>
        /// Returns list of paternal uncles for the provided person.
        ///</summary>
        public List<FamilyTreeNode> GetPaternalUncles(string name)
        {            
            if(!map.ContainsKey(name))
                throw new PersonNotFoundException();
            
            FamilyTreeNode currentMember = map[name];
        
            List<FamilyTreeNode> paternalUncles = currentMember.Father?.Mother?.Children?.Where(x => x.Gender == Gender.Male && x.Name != currentMember.Father.Name)?.ToList();
            
            if(paternalUncles == null || paternalUncles.Count == 0)
                throw new NoRelationExistsException();
                
            return paternalUncles;           
        }
        
        ///<summary>
        /// Returns list of paternal aunts for the provided person.
        ///</summary>
        public List<FamilyTreeNode> GetPaternalAunts(string name)
        {            
            if(!map.ContainsKey(name))
                throw new PersonNotFoundException();
            
            FamilyTreeNode currentMember = map[name];
            
            List<FamilyTreeNode> paternalAunts = currentMember.Father?.Father?.Children?.Where(x => x.Gender == Gender.Female)?.ToList();
            
            if(paternalAunts == null || paternalAunts.Count == 0)
                throw new NoRelationExistsException();
                
            return paternalAunts;           
        }
        
        ///<summary>
        /// Returns list of maternal uncles for the provided person.
        ///</summary>
        public List<FamilyTreeNode> GetMaternalUncles(string name)
        {            
            if(!map.ContainsKey(name))
                throw new PersonNotFoundException();
            
            FamilyTreeNode currentMember = map[name];
            
            List<FamilyTreeNode> maternalUncles = currentMember.Mother?.Mother?.Children?.Where(x => x.Gender == Gender.Male)?.ToList();
            
            if(maternalUncles == null || maternalUncles.Count == 0)
                throw new NoRelationExistsException();
                
            return maternalUncles;            
        }
        
        ///<summary>
        /// Returns list of maternal aunts for the provided person.
        ///</summary>
        public List<FamilyTreeNode> GetMaternalAunts(string name)
        {            
            if(!map.ContainsKey(name))
                throw new PersonNotFoundException();
            
            FamilyTreeNode currentMember = map[name];
            
            List<FamilyTreeNode> maternalAunts = currentMember.Mother?.Mother?.Children?.Where(x => x.Gender == Gender.Female && x.Name != currentMember.Mother.Name)?.ToList();
            
            if(maternalAunts == null || maternalAunts.Count == 0)
                throw new NoRelationExistsException();
                
            return maternalAunts;            
        }
        
        ///<summary>
        /// Returns list of sister-in-laws for the provided person.
        ///</summary>
        public List<FamilyTreeNode> GetSisterInLaws(string name)
        {            
            if(!map.ContainsKey(name))
                throw new PersonNotFoundException();
            
            FamilyTreeNode currentMember = map[name];
            
            // Sister-In-Law from spouse's side (spouse's sisters)
            List<FamilyTreeNode> sisterInLawsFromSpouse = currentMember.Spouse?.Mother?.Children?.Where(x => x.Gender == Gender.Female && x.Name != currentMember.Spouse.Name)?.ToList();
            // Sister-In-Law from sibling's side (sibling's wife)
            List<FamilyTreeNode> sisterInLawsFromSiblings = GetSiblings(name)?.Where(x => x.Gender == Gender.Male && x.Spouse != null)?.Select(x => x.Spouse)?.ToList();
            
            if((sisterInLawsFromSpouse == null || sisterInLawsFromSpouse.Count == 0) && (sisterInLawsFromSiblings == null || sisterInLawsFromSiblings.Count == 0))
                throw new NoRelationExistsException();

            List<FamilyTreeNode> result = sisterInLawsFromSpouse != null ? sisterInLawsFromSpouse :  new List<FamilyTreeNode>();
            result.AddRange(sisterInLawsFromSiblings);	

            return result;            
        }
        
        ///<summary>
        /// Returns list of brother-in-laws for the provided person.
        ///</summary>
        public List<FamilyTreeNode> GetBrotherInLaws(string name)
        {            
            if(!map.ContainsKey(name))
                throw new PersonNotFoundException();
            
            FamilyTreeNode currentMember = map[name];
            
            // Brother-In-Law from spouse's side (spouse's brothers)
            List<FamilyTreeNode> brotherInLawsFromSpouse = currentMember.Spouse?.Mother?.Children?.Where(x => x.Gender == Gender.Male && x.Name != currentMember.Spouse.Name)?.ToList();
            // Brother-In-Law from sibling's side (sibling's husband)
            List<FamilyTreeNode> brotherInLawsFromSiblings = GetSiblings(name)?.Where(x => x.Gender == Gender.Female && x.Spouse != null)?.Select(x => x.Spouse)?.ToList();
            
            if((brotherInLawsFromSpouse == null || brotherInLawsFromSpouse.Count == 0) && (brotherInLawsFromSiblings == null || brotherInLawsFromSiblings.Count == 0))
                throw new NoRelationExistsException();

            List<FamilyTreeNode> result = brotherInLawsFromSpouse != null ? brotherInLawsFromSpouse : new List<FamilyTreeNode>();
            result.AddRange(brotherInLawsFromSiblings);
                
            return result;           
        }
        
        ///<summary>
        /// Returns list of sons for the provided person.
        ///</summary>
        public List<FamilyTreeNode> GetSons(string name)
        {            
            if(!map.ContainsKey(name))
                throw new PersonNotFoundException();
            
            FamilyTreeNode currentMember = map[name];
            
            List<FamilyTreeNode> sons = currentMember.Children.Where(x => x.Gender == Gender.Male)?.ToList();
            
            if(sons == null || sons.Count == 0)
                throw new NoRelationExistsException();
                
            return sons;	
        }
        
        ///<summary>
        /// Returns list of daughters for the provided person.
        ///</summary>
        public List<FamilyTreeNode> GetDaughters(string name)
        {            
            if(!map.ContainsKey(name))
                throw new PersonNotFoundException();
            
            FamilyTreeNode currentMember = map[name];
            
            List<FamilyTreeNode> daughters = currentMember.Children.Where(x => x.Gender == Gender.Female)?.ToList();
            
            if(daughters == null || daughters.Count == 0)
                throw new NoRelationExistsException();
                
            return daughters;	
        }
        
        ///<summary>
        /// Returns list of siblings for the provided person.
        ///</summary>
        public List<FamilyTreeNode> GetSiblings(string name)
        {            
            if(!map.ContainsKey(name))
                throw new PersonNotFoundException();
            
            FamilyTreeNode currentMember = map[name];
            
            List<FamilyTreeNode> siblings = currentMember.Mother?.Children?.Where(x => x.Name != currentMember.Name)?.ToList();
            
            if(siblings == null || siblings.Count == 0)
                throw new NoRelationExistsException();
                
            return siblings;	
        }   
    }
}