using Olympics.Core.Repository.Interfaces;
using Olympics.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Business.Specifications.AbStrations
{
	public interface ICustomerSpecification:ISpecification<Customer>
	{
		ICustomerSpecification WithName(string name);

		ICustomerSpecification WithAge(int age);
	}
}
