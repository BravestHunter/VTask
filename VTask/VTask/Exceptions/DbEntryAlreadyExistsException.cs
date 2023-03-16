using System;

namespace VTask.Exceptions
{
    public class DbEntryAlreadyExistsException : Exception
    {
        public DbEntryAlreadyExistsException(string message = "") : base(message) { }
    }
}
