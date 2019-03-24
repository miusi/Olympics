using Olympics.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olympics.Core.Repository.EntityFramework
{
	public class EntityFrameworkUnitOfWorkConvertor : IUnitOfWorkConvertor
	{
		public IQueryable<TEntity> ToQueryable<TEntity>(IUnitOfWork unitOfWork) where TEntity : class
		{
			return (unitOfWork as EntityFrameworkUnitOfWork).DbContext.Set<TEntity>();
		}
	}
}
