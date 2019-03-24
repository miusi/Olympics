using Olympics.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Olympics.Core.Repository.EntityFramework
{
	public class EntityFrameworkTransaction : ITransaction
	{
		public EntityFrameworkTransaction(EntityFrameworkUnitOfWork unitOfWork)
		{
			this.UnitOfWork = unitOfWork;

			this.TransactionScope = new TransactionScope();
		}

		protected EntityFrameworkUnitOfWork UnitOfWork { get; private set; }

		protected TransactionScope TransactionScope { get; private set; }

		public void Commit()
		{
			this.UnitOfWork.Flush();
			this.TransactionScope.Complete();
		}

		public void Dispose()
		{
			if (this.TransactionScope != null)
			{
				(this.TransactionScope as IDisposable).Dispose();
				this.TransactionScope = null;
				this.UnitOfWork = null;
			}
		}

		public void Rollback()
		{
			
		}
	}
}
