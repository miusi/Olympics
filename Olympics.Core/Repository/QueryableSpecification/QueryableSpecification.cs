using Olympics.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olympics.Core.Repository.QueryableSpecification
{
	public abstract class QueryableSpecification<TEntity> : ISpecification<TEntity>
		where TEntity : class
	{
		protected QueryableSpecification(IUnitOfWorkConvertor unitOfWorkConvertor)
		{
			if (unitOfWorkConvertor == null)
			{
				throw new ArgumentNullException("unitOfWorkConvertor");
			}
			this.UnitOfWorkConvertor = unitOfWorkConvertor;
		}

		protected IQueryable<TEntity> Queryable { get; set; }
		protected IUnitOfWorkConvertor UnitOfWorkConvertor { get; private set; }

		public void Initialize(IUnitOfWork unitOfWork)
		{
			this.Queryable = this.UnitOfWorkConvertor.ToQueryable<TEntity>(unitOfWork);
		}

		public ISpecificationResult<TEntity> ToResult()
		{
			return new QueryableSpecificationResult<TEntity>(Queryable);
		}
	}
}
