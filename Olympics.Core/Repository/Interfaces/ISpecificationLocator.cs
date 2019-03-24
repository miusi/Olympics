using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Core.Repository.Interfaces
{
	public interface ISpecificationLocator
	{
		TSpecification Resolve<TSpecification, TEntity>() 
			where TSpecification : ISpecification<TEntity>
			where TEntity : class;
	}
}
