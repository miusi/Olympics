using Olympics.Core.Repository.Exceptions;
using Olympics.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Core.Repository.GenericRepository
{
	public class GenericRepository<TEntity, TPrimaryKey> : IGenericRepository<TEntity, TPrimaryKey> where TEntity : class
	{
		public GenericRepository(IUnitOfWork unitOfWork
			,ISpecificationLocator specificationLocator)
		{
			this.EnsureNotNull(specificationLocator);
			this.EnsureNotNull(unitOfWork);

			this.SpecificationLocator = specificationLocator;
			this.UnitOfWork = unitOfWork;
		}

		protected void EnsureNotNull(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o", "Argument can not be null.");
			}
		}

		protected ISpecificationLocator SpecificationLocator { get; private set; }
		protected IUnitOfWork UnitOfWork { get; private set; }

		public virtual Task Delete(TEntity entity)
		{
			return this.UnitOfWork.Insert(entity);
		}

		public Task<IList<TEntity>> GetAll()
		{
			return this.UnitOfWork.GetAll<TEntity>();
		}

		public Task<TEntity> GetById(TPrimaryKey id)
		{
			return this.UnitOfWork.GetById<TEntity,TPrimaryKey>(id);
		}

		public Task Insert(TEntity entity)
		{
			return this.UnitOfWork.Insert(entity);
		}

		public Task Update(TEntity entity)
		{
			return this.UnitOfWork.Update(entity);
		}

		Task<TSpecification> IGenericRepository<TEntity, TPrimaryKey>.Specify<TSpecification>()
		{
			TSpecification specification = default(TSpecification);
			try
			{
				specification = this.SpecificationLocator.Resolve<TSpecification, TEntity>();
			}
			catch (Exception ex)
			{
				throw new GenericRepositoryException(
					string.Format(
						"Could not resolve requested specification {0} for entity {1} from the specification locator."
						, typeof(TSpecification).FullName
						, typeof(TEntity).FullName
						)
					, ex
					);
			}
			specification.Initialize(UnitOfWork);
			return Task.FromResult(specification);
		}
	}
}
