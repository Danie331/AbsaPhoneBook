
using System;

namespace Absa.Models.Exceptions
{
    [Serializable]
    public class DatastoreException : Exception
    {
        public DatastoreException()
        {
        }

        public DatastoreException(string message)
            : base(message)
        {
        }

        public DatastoreException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}