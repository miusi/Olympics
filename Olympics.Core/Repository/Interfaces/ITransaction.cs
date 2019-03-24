using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Core.Repository.Interfaces
{
	public interface ITransaction:IDisposable
	{
		/// <summary>
		/// commit
		/// </summary>
		void Commit();

		/// <summary>
		/// rollback
		/// </summary>
		void Rollback();
	}
}
