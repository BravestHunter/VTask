using System;

namespace VTask.Exceptions
{
    public class DbEntryNotFoundException : Exception
    {
        public DbEntryNotFoundException(string message = "") : base(message) { }
    }
}
