using System;

namespace VTask.Exceptions
{
    public class DbEntryAlreadyExists : Exception
    {
        public DbEntryAlreadyExists(string message = "") : base(message) { }
    }
}
