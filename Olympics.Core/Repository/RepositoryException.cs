using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Core.Repository
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}
