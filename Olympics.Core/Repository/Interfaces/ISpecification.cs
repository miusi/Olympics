using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Core.Repository.Interfaces
{
	public interface ISpecification<TEntity> where TEntity : class
	{
		void Initialize(IUnitOfWork unitOfWork);

		ISpecificationResult<TEntity> ToResult();

	}
}
