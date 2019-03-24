using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Olympics.Core.Repository.Interfaces
{
	public interface ISpecificationResult<TEntity> where TEntity : class
	{
		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		ISpecificationResult<TEntity> Take(int count);

		/// <summary>
		/// 获取指定位置
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		ISpecificationResult<TEntity> Skip(int count);

		/// <summary>
		/// 正序排序
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="keySelector"></param>
		/// <returns></returns>
		ISpecificationResult<TEntity> OrderByAscending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

		/// <summary>
		/// 倒序排序
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="keySelector"></param>
		/// <returns></returns>
		ISpecificationResult<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> keySelector);

		/// <summary>
		/// ToList
		/// </summary>
		/// <returns></returns>
		IList<TEntity> ToList();

		/// <summary>
		/// Single
		/// </summary>
		/// <returns></returns>
		TEntity Single();

		/// <summary>
		/// SingleOrDefault
		/// </summary>
		/// <returns></returns>
		TEntity SingleOrDefault();
	}
}
