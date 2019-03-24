using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Core.Repository.Exceptions
{
	public class GenericRepositoryException : Exception
	{
		public GenericRepositoryException()
		: base()
		{
		}

		public GenericRepositoryException(string message)
		: base(message)
		{
		}

		public GenericRepositoryException(string message, Exception innerException)
		: base(message, innerException)
		{
		}
	}
}
