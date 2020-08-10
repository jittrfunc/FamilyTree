using System;
using System.Collections.Generic;
using FamilyTree.Core.Enums;

namespace FamilyTree.Core.DataStructures
{
    ///<summary>
    /// This class represents an individual in the family tree
    ///</summary>
    public class FamilyTreeNode
    {
        public string Name {get; set;}
        public Gender Gender {get; set;}      
        public FamilyTreeNode Father {get; set;}
        public FamilyTreeNode Mother {get; set;}   
        public FamilyTreeNode Spouse {get; set;}
        public List<FamilyTreeNode> Children {get; set;}
        
        public FamilyTreeNode(string name, Gender gender)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Validation Error: Please provide a non-null, non-empty value for name.");

            Name = name;
            Gender = gender;
            Children = new List<FamilyTreeNode>();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != GetType()) return false;

            return Name == ((FamilyTreeNode)obj).Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }     
    }
}