using System.Collections.Generic;
using FamilyTree.Core.Enums;
using FamilyTree.Core.DataStructures;

namespace FamilyTree.Core.Interfaces
{
    ///<summary>
    /// Interface/contract for the FamilyTreeGraph data structure.
    ///</summary>
    public interface IFamilyTreeGraph
    {
        void AddSpouse(string husbandName, string wifeName);
        void AddChild(string motherName, string childName, Gender childGender);
        List<FamilyTreeNode> GetPaternalUncles(string name);
        List<FamilyTreeNode> GetPaternalAunts(string name);
        List<FamilyTreeNode> GetMaternalUncles(string name);
        List<FamilyTreeNode> GetMaternalAunts(string name);
        List<FamilyTreeNode> GetSisterInLaws(string name);
        List<FamilyTreeNode> GetBrotherInLaws(string name);
        List<FamilyTreeNode> GetSons(string name);
        List<FamilyTreeNode> GetDaughters(string name);
        List<FamilyTreeNode> GetSiblings(string name);
    }
}