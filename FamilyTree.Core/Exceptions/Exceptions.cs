using System;
using FamilyTree.Core.Constants;

namespace FamilyTree.Core.Exceptions
{
    ///<summary>
    /// Exception which is thrown when addition of child failed.
    ///</summary>
    public class ChildAdditionFailedException : Exception
    {
        public ChildAdditionFailedException() : base(MessageConstants.CHILD_ADDITION_FAILURE_MESSAGE) { }
    }

    ///<summary>
    /// Exception which is thrown when no relation was found for the query.
    ///</summary>
    public class NoRelationExistsException : Exception
    {
        public NoRelationExistsException() : base(MessageConstants.NO_RELATION_EXISTS_MESSAGE) { }
    }

    ///<summary>
    /// Exception which is thrown when addition of spouse failed.
    ///</summary>
    public class SpouseAdditionFailedException : Exception
    {
        public SpouseAdditionFailedException() : base(MessageConstants.SPOUSE_ADDITION_FAILURE_MESSAGE) { }
    }

    ///<summary>
    /// Exception which is thrown when person is not present in the family tree.
    ///</summary>
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException() : base(MessageConstants.PERSON_NOT_FOUND_MESSAGE) { }
    }
}