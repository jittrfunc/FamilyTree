using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FamilyTree.Core.DataStructures;
using FamilyTree.Core.Exceptions;
using FamilyTree.Core.Enums;
using FamilyTree.Core.Constants;

namespace FamilyTree.ConsoleUtilities
{
    ///<summary>
    /// Parses the input file and adds the relationships into a FamilyTreeGraph data structure,
    /// computes the relationship queries and maps the output produced by the data structure to the expected console output format.
    ///</summary>
    public class FamilyTreeExecutor
    {
        private FamilyTreeGraph familyTreeGraph;
        private string inputFile;

        public FamilyTreeExecutor(string inputFile)
        {
            this.familyTreeGraph = new FamilyTreeGraph();
            this.inputFile = inputFile;
        }

        public void Run()
        {
            string[] lines = File.ReadAllLines(inputFile);

            foreach(string line in lines.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                string[] input = line.Split(' ');
                string command = input[0];

                switch(command)
                {
                    case "ADD_SPOUSE":
                        string husbandName = input[1];
                        string wifeName = input[2];
                        AddSpouse(husbandName, wifeName);
                        break;

                    case "ADD_CHILD":
                        string motherName = input[1];
                        string childName = input[2];
                        string childGender = input[3];
                        AddChild(motherName, childName, childGender);
                        break;

                    case "GET_RELATIONSHIP":
                        string name = input[1];
                        string relationship = input[2];

                        switch(relationship)
                        {
                            case "Paternal-Uncle":
                                GetPaternalUncles(name);
                                break;
                            case "Maternal-Uncle":
                                GetMaternalUncles(name);
                                break;
                            case "Paternal-Aunt":
                                GetPaternalAunts(name);
                                break;
                            case "Maternal-Aunt":
                                GetMaternalAunts(name);
                                break;
                            case "Sister-In-Law":
                                GetSisterInLaws(name);
                                break;
                            case "Brother-In-Law":
                                GetBrotherInLaws(name);
                                break;
                            case "Son":
                                GetSons(name);
                                break;
                            case "Daughter":
                                GetDaughters(name);
                                break;
                            case "Siblings":
                                GetSiblings(name);
                                break;
                            default:
                                System.Console.WriteLine("Unsupported relationship uncountered. Valid relationships include - Paternal-Uncle, Maternal-Uncle, Paternal-Aunt, Maternal-Aunt, Sister-In-Law, Brother-In-Law, Son, Daughter, Siblings.");
                                break;
                        }

                        break;
                    default:
                        System.Console.WriteLine("Unsupported command uncountered. Valid commands include - ADD_SPOUSE, ADD_CHILD, GET_RELATIONSHIP");
                        break;                           
                }
            }
        }

        private void AddSpouse(string husbandName, string wifeName)
        {
            try
            {
                familyTreeGraph.AddSpouse(husbandName, wifeName);
                System.Console.WriteLine(MessageConstants.SPOUSE_ADDITION_SUCCESS_MESSAGE);
            }
            catch(SpouseAdditionFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }

        private void AddChild(string motherName, string childName, string childGender)
        {
            try
            {
                string genderStrNormalized = childGender[0].ToString().ToUpper() + childGender.Substring(1);

                if(genderStrNormalized != "Male" && genderStrNormalized != "Female")
                    throw new ChildAdditionFailedException();

                familyTreeGraph.AddChild(motherName, childName, Enum.Parse<Gender>(genderStrNormalized));
                System.Console.WriteLine(MessageConstants.CHILD_ADDITION_SUCCESS_MESSAGE);
            }
            catch(ChildAdditionFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }          
        } 

        private void GetPaternalUncles(string name)
        {
            try
            {
                List<FamilyTreeNode> paternalUncles = familyTreeGraph.GetPaternalUncles(name);

                foreach(FamilyTreeNode node in paternalUncles)
                    Console.Write(node + " ");

                Console.Write("\n");
            }
            catch(PersonNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }

        private void GetPaternalAunts(string name)
        {
            try
            {
                List<FamilyTreeNode> paternalAunts = familyTreeGraph.GetPaternalAunts(name);

                foreach(FamilyTreeNode person in paternalAunts)
                    Console.Write(person + " ");

                Console.Write("\n");
            }
            catch(PersonNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }

        private void GetMaternalUncles(string name)
        {
            try
            {
                List<FamilyTreeNode> maternalUncles = familyTreeGraph.GetMaternalUncles(name);

                foreach(FamilyTreeNode person in maternalUncles)
                    Console.Write(person + " ");

                Console.Write("\n");
            }
            catch(PersonNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }

        private void GetMaternalAunts(string name)
        {
            try
            {
                List<FamilyTreeNode> maternalAunts = familyTreeGraph.GetMaternalAunts(name);

                foreach(FamilyTreeNode person in maternalAunts)
                    Console.Write(person + " ");

                Console.Write("\n");
            }
            catch(PersonNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }

        private void GetSisterInLaws(string name)
        {
            try
            {
                List<FamilyTreeNode> sisterInLaws = familyTreeGraph.GetSisterInLaws(name);

                foreach(FamilyTreeNode person in sisterInLaws)
                    Console.Write(person + " ");

                Console.Write("\n");
            }
            catch(PersonNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }

        private void GetBrotherInLaws(string name)
        {
            try
            {
                List<FamilyTreeNode> brotherInLaws = familyTreeGraph.GetBrotherInLaws(name);

                foreach(FamilyTreeNode person in brotherInLaws)
                    Console.Write(person + " ");

                Console.Write("\n");
            }
            catch(PersonNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }

        private void GetSons(string name)
        {
            try
            {
                List<FamilyTreeNode> sons = familyTreeGraph.GetSons(name);

                foreach(FamilyTreeNode person in sons)
                    Console.Write(person + " ");

                Console.Write("\n");
            }
            catch(PersonNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }

        private void GetDaughters(string name)
        {
            try
            {
                List<FamilyTreeNode> daughters = familyTreeGraph.GetDaughters(name);

                foreach(FamilyTreeNode person in daughters)
                    Console.Write(person + " ");

                Console.Write("\n");
            }
            catch(PersonNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }

        private void GetSiblings(string name)
        {
            try
            {
                List<FamilyTreeNode> siblings = familyTreeGraph.GetSiblings(name);

                foreach(FamilyTreeNode person in siblings)
                    Console.Write(person + " ");

                Console.Write("\n");
            }
            catch(PersonNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch(NoRelationExistsException ex)
            {
                 System.Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.Error.Write(ex.Message);
            }            
        }
    }
}