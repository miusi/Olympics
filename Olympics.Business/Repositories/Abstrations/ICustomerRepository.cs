using Olympics.Core.Repository.Interfaces;
using Olympics.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Business.Repositories.Abstrations
{
	interface ICustomerRepository:IGenericRepository<Customer,int>
	{
	}
}
