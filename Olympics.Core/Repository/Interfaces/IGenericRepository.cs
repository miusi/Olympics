using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Core.Repository.Interfaces
{
	public interface IGenericRepository<TEntity,TPrimaryKey> where TEntity:class
	{
		/// <summary>
		/// 新增
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task Insert(TEntity entity);

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task Update(TEntity entity);

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task Delete(TEntity entity);

		/// <summary>
		/// 获取单个对象
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Task<TEntity> GetById(TPrimaryKey id);

		/// <summary>
		/// 获取所有对象
		/// </summary>
		/// <returns></returns>
		Task<IList<TEntity>> GetAll();

		/// <summary>
		/// 条件获取
		/// </summary>
		/// <typeparam name="TSpecification"></typeparam>
		/// <returns></returns>
		Task<TSpecification> Specify<TSpecification>() where TSpecification : class, ISpecification<TEntity>;
	}
}
