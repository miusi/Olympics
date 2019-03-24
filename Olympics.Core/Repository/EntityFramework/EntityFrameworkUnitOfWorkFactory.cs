using Microsoft.EntityFrameworkCore; 
using Olympics.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Core.Repository.EntityFramework
{
	class EntityFrameworkUnitOfWorkFactory : IUnitOfWorkFactory
	{ 

		public EntityFrameworkUnitOfWorkFactory(EntityFrameworkDbContext dbContext)
		{
			DbContext = dbContext;
		}

		protected EntityFrameworkDbContext DbContext { get; private set; }

		public IUnitOfWork BeginUnitOfWork()
		{
			return new EntityFrameworkUnitOfWork(
				 DbContext
				);
		}

		

		public void EndUnitOfWork(IUnitOfWork unitOfWork)
		{
			var entityFrameworkUnitOfWork = unitOfWork as EntityFrameworkUnitOfWork;
			if (entityFrameworkUnitOfWork != null)
			{
				entityFrameworkUnitOfWork.Dispose();
				entityFrameworkUnitOfWork = null;
			}
		}
	}
}
