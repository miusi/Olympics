using Olympics.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olympics.Core.Repository.QueryableSpecification
{
	public class QueryableSpecificationResult<TEntity> : ISpecificationResult<TEntity>
		where TEntity:class
	{
		public QueryableSpecificationResult(IQueryable<TEntity> queryable)
		{
			this.Queryable = queryable;
		}

		protected IQueryable<TEntity> Queryable { get; set; }

		public ISpecificationResult<TEntity> OrderByAscending<TKey>(System.Linq.Expressions.Expression<Func<TEntity, TKey>> keySelector)
		{
			this.Queryable = this.Queryable.OrderBy(keySelector);
			return this;
		}

		public ISpecificationResult<TEntity> OrderByDescending<TKey>(System.Linq.Expressions.Expression<Func<TEntity, TKey>> keySelector)
		{
			this.Queryable = this.Queryable.OrderByDescending(keySelector);
			return this;
		}

		public TEntity Single()
		{
			return this.Queryable.Single();
		}

		public TEntity SingleOrDefault()
		{
			return this.Queryable.SingleOrDefault();
		}

		public ISpecificationResult<TEntity> Skip(int count)
		{
			this.Queryable = this.Queryable.Skip(count);
			return this;
		}

		public ISpecificationResult<TEntity> Take(int count)
		{
			this.Queryable = this.Queryable.Take(count);
			return this;
		}

		public IList<TEntity> ToList()
		{
			return this.Queryable.ToList();
		}
	}
}
