using System;
using FamilyTree.Core.DataStructures;
using FamilyTree.Core.Enums;

namespace FamilyTree.Tests
{
    ///<summary>
    /// This class provides the test data or fixture to be used when testing the family tree functionalities.
    ///</summary>
    public class FamilyTreeTestFixture : IDisposable
    {
        public FamilyTreeGraph familyTreeGraph { get; set; }
        public FamilyTreeTestFixture()
        {
            familyTreeGraph = CreateFamilyGraph();
        }

        public void Dispose() {}

        private FamilyTreeGraph CreateFamilyGraph()
        {
            FamilyTreeGraph graph = new FamilyTreeGraph();
            
            graph.AddSpouse("Shan", "Anga");
            graph.AddChild("Anga", "Chit", Gender.Male);
            graph.AddChild("Anga", "Ish", Gender.Male);
            graph.AddChild("Anga", "Vich", Gender.Male);
            graph.AddChild("Anga", "Aras", Gender.Male);
            graph.AddChild("Anga", "Satya", Gender.Female);
            graph.AddSpouse("Chit", "Amba");
            graph.AddSpouse("Vich", "Lika");
            graph.AddSpouse("Aras", "Chitra");
            graph.AddSpouse("Vyan", "Satya");
            graph.AddChild("Amba", "Dritha", Gender.Female);
            graph.AddChild("Amba", "Tritha", Gender.Female);
            graph.AddChild("Amba", "Vritha", Gender.Male);
            graph.AddChild("Lika", "Vila", Gender.Female);
            graph.AddChild("Lika", "Chika", Gender.Female);
            graph.AddChild("Chitra", "Jnki", Gender.Female);
            graph.AddChild("Chitra", "Ahit", Gender.Male);
            graph.AddChild("Satya", "Asva", Gender.Male);
            graph.AddChild("Satya", "Vyas", Gender.Male);
            graph.AddChild("Satya", "Atya", Gender.Female);
            graph.AddSpouse("Jaya", "Dritha");
            graph.AddSpouse("Arit", "Jnki");
            graph.AddSpouse("Asva", "Satvy");
            graph.AddSpouse("Vyas", "Krpi");
            graph.AddChild("Dritha", "Yodhan", Gender.Male);
            graph.AddChild("Jnki", "Laki", Gender.Male);
            graph.AddChild("Jnki", "Lavnya", Gender.Female);
            graph.AddChild("Satvy", "Vasa", Gender.Male);
            graph.AddChild("Krpi", "Kriya", Gender.Male);
            graph.AddChild("Satya", "Krithi", Gender.Female);

            return graph;
        }

    }
}