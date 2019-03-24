using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Core.Repository.Interfaces
{
	public interface IUnitOfWorkFactory
	{
		IUnitOfWork BeginUnitOfWork();

		void EndUnitOfWork(IUnitOfWork unitOfWork);
	}
}
