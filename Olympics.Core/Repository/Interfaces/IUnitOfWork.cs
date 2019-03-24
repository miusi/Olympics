using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Core.Repository.Interfaces
{
	public interface IUnitOfWork:IDisposable
	{
		/// <summary>
		/// Flushes content of unit of work to the underlying data storage. Causes unsaved
		/// </summary>
		Task<int> Flush();

		/// <summary>
		/// 开启事务
		/// </summary>
		/// <returns></returns>
		ITransaction BeginTransaction();

		/// <summary>
		/// 结束事务
		/// </summary>
		/// <param name="transaction"></param>
		void EndTransaction(ITransaction transaction);

		/// <summary>
		/// 数据插入
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task Insert<TEntity>(TEntity entity) where TEntity : class;

		/// <summary>
		/// 数据更新
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task Update<TEntity>(TEntity entity) where TEntity : class;

		/// <summary>
		/// 删除
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task Delete<TEntity>(TEntity entity) where TEntity : class;

		/// <summary>
		/// 根据Id获取
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <typeparam name="TPrimaryKey"></typeparam>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<TEntity> GetById<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : class;

		/// <summary>
		/// 获取所有
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <returns></returns>
		Task<List<TEntity>> GetAll<TEntity>() where TEntity : class;
	}
}
