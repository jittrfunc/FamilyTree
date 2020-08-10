using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using FamilyTree.Core.DataStructures;
using FamilyTree.Core.Exceptions;
using FamilyTree.Core.Enums;
using FamilyTree.Core.Constants;

namespace FamilyTree.Tests
{
    ///<summary>
    /// This class tests the functionalities provided by the FamilyTreeGraph data structure
    ///</summary>
    public class FamilyTreeTests : IClassFixture<FamilyTreeTestFixture>
    {
        private FamilyTreeTestFixture testFixture = new FamilyTreeTestFixture();

        public FamilyTreeTests(FamilyTreeTestFixture testFixture)
        {
            this.testFixture = testFixture;
        }

        
        [Fact]
        public void TestAddNewCouple()
        {
            FamilyTreeGraph graph = new FamilyTreeGraph();
            graph.AddSpouse("Shan", "Anga");
        }

        [Fact]
        public void TestAddWifeToExistingCouple()
        {
            FamilyTreeGraph graph = new FamilyTreeGraph();
            graph.AddSpouse("Shan", "Anga");
            var ex = Assert.Throws<SpouseAdditionFailedException>(() => graph.AddSpouse("Shan", "Tima"));
            Assert.Equal(MessageConstants.SPOUSE_ADDITION_FAILURE_MESSAGE, ex.Message);
        }

        [Fact]
        public void TestAddHusbandToExistingCouple()
        {
            FamilyTreeGraph graph = new FamilyTreeGraph();
            graph.AddSpouse("Shan", "Anga");
            var ex = Assert.Throws<SpouseAdditionFailedException>(() => graph.AddSpouse("Kuru", "Anga"));
            Assert.Equal(MessageConstants.SPOUSE_ADDITION_FAILURE_MESSAGE, ex.Message);
        }

        [Fact]
        public void TestAddChildToExistingMother()
        {
            FamilyTreeGraph graph = new FamilyTreeGraph();
            graph.AddSpouse("Shan", "Anga");
            graph.AddChild("Anga", "Chit", Gender.Male);
        }

        [Fact]
        public void TestAddChildToNonExistingMember()
        {
            FamilyTreeGraph graph = new FamilyTreeGraph();
            var ex = Assert.Throws<PersonNotFoundException>(() => graph.AddChild("Tiri", "Chit", Gender.Male));
            Assert.Equal(MessageConstants.PERSON_NOT_FOUND_MESSAGE, ex.Message);
        }

        [Fact]
        public void TestAddChildToExistingMale()
        {
            FamilyTreeGraph graph = new FamilyTreeGraph();
            graph.AddSpouse("Shan", "Anga");        
            var ex = Assert.Throws<ChildAdditionFailedException>(() => graph.AddChild("Shan", "Chit", Gender.Male));
            Assert.Equal(MessageConstants.CHILD_ADDITION_FAILURE_MESSAGE, ex.Message);
        }

        [Fact]
        public void TestGetRelationshipPaternalUncle()
        {
            FamilyTreeGraph graph = testFixture.familyTreeGraph; 
            var ex = Assert.Throws<NoRelationExistsException>(() => graph.GetPaternalUncles("Laki"));
            Assert.Equal(MessageConstants.NO_RELATION_EXISTS_MESSAGE, ex.Message);
        }

        [Fact]
        public void TestGetRelationshipMaternalUncle()
        {
            FamilyTreeGraph graph = testFixture.familyTreeGraph; 
            var relations = graph.GetMaternalUncles("Laki");
            Assert.Equal("Ahit", relations.Single().Name);
        }

        [Fact]
        public void TestGetRelationshipSon()
        {
            FamilyTreeGraph graph = testFixture.familyTreeGraph; 
            var relations = graph.GetSons("Satya");

            var expected = new List<FamilyTreeNode>
            {
                new FamilyTreeNode("Asva", Gender.Male),
                new FamilyTreeNode("Vyas", Gender.Male)
            };

            if(relations == null || relations.Count != expected.Count)
                Assert.True(false, "Test failed for TestGetRelationshipSon.");

            for(int i = 0; i < expected.Count; i++)
                if(relations[i].Name != expected[i].Name)
                    Assert.True(false, "Test failed for TestGetRelationshipSon.");
        }

        [Fact]
        public void TestGetRelationshipBrotherInLaw()
        {
            FamilyTreeGraph graph = testFixture.familyTreeGraph; 
            var relations = graph.GetBrotherInLaws("Tritha");
            Assert.Equal("Jaya", relations.Single().Name);
        }

        [Fact]
        public void TestGetRelationshipSisterInLaw()
        {
            FamilyTreeGraph graph = testFixture.familyTreeGraph; 
            var relations = graph.GetSisterInLaws("Atya");

            var expected = new List<FamilyTreeNode>
            {
                new FamilyTreeNode("Satvy", Gender.Female),
                new FamilyTreeNode("Krpi", Gender.Male)
            };

            if(relations == null || relations.Count != expected.Count)
                Assert.True(false, "Test failed for TestGetRelationshipSisterInLaw.");

            for(int i = 0; i < expected.Count; i++)
                if(relations[i].Name != expected[i].Name)
                    Assert.True(false, "Test failed for TestGetRelationshipSisterInLaw.");
        }

        [Fact]
        public void TestGetRelationshipSiblings()
        {
            FamilyTreeGraph graph = testFixture.familyTreeGraph; 
            var relations = graph.GetSiblings("Vich");
            
            var expected = new List<FamilyTreeNode>
            {
                new FamilyTreeNode("Chit", Gender.Female),
                new FamilyTreeNode("Ish", Gender.Male),
                new FamilyTreeNode("Aras", Gender.Female),
                new FamilyTreeNode("Satya", Gender.Female)
            };

            if(relations == null || relations.Count != expected.Count)
                Assert.True(false, "Test failed for TestGetRelationshipSiblings.");

            for(int i = 0; i < expected.Count; i++)
                if(relations[i].Name != expected[i].Name)
                    Assert.True(false, "Test failed for TestGetRelationshipSiblings.");

        }

        [Fact]
        public void TestGetRelationshipDaughters()
        {
            FamilyTreeGraph graph = testFixture.familyTreeGraph; 
            var ex = Assert.Throws<PersonNotFoundException>(() => graph.GetDaughters("Misha"));
            Assert.Equal(MessageConstants.PERSON_NOT_FOUND_MESSAGE, ex.Message);
        }

        [Fact]
        public void TestGetRelationshipPaternalAunt()
        {
            FamilyTreeGraph graph = testFixture.familyTreeGraph; 
            var relations = graph.GetPaternalAunts("Chika");
            Assert.Equal("Satya", relations.Single().Name);
        }

        [Fact]
        public void TestGetRelationshipMaternalAunt()
        {
            FamilyTreeGraph graph = testFixture.familyTreeGraph; 
            var relations = graph.GetMaternalAunts("Yodhan");
            Assert.Equal("Tritha", relations.Single().Name);
        }        
    }
}
