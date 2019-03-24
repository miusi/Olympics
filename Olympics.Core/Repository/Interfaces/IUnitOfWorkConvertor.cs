using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olympics.Core.Repository.Interfaces
{
	public interface IUnitOfWorkConvertor
	{
		/// <summary>
		/// 转换为查询对象
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="unitOfWork"></param>
		/// <returns></returns>
		IQueryable<TEntity> ToQueryable<TEntity>(IUnitOfWork unitOfWork) where TEntity : class;
	}
}
