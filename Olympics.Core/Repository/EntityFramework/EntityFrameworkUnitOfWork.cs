using Microsoft.EntityFrameworkCore;
using Olympics.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Core.Repository.EntityFramework
{
	public class EntityFrameworkUnitOfWork : IUnitOfWork
	{
		public EntityFrameworkUnitOfWork(DbContext dbContext)
		{
			DbContext = dbContext;
		}

		public DbContext DbContext { get; protected set; }

		public ITransaction BeginTransaction()
		{
			return new EntityFrameworkTransaction(this);
		}
		public Task Insert<TEntity>(TEntity entity) where TEntity : class
		{
			this.DbContext.Set<TEntity>().AddAsync(entity);
			return Task.CompletedTask;
		}

		public Task Update<TEntity>(TEntity entity) where TEntity : class
		{
			this.DbContext.Set<TEntity>().Attach(entity);
			return Task.CompletedTask;
		}
		public Task Delete<TEntity>(TEntity entity) where TEntity : class
		{
			this.DbContext.Set<TEntity>().Remove(entity);
			return Task.CompletedTask;
		}
		 
		public void EndTransaction(ITransaction transaction)
		{
			if (transaction != null)
			{
				(transaction as IDisposable).Dispose();
				transaction = null;
			}
		}

		public Task<int> Flush()
		{
			return this.DbContext.SaveChangesAsync();
		}

		public Task<List<TEntity>> GetAll<TEntity>() where TEntity : class
		{
			return this.DbContext.Set<TEntity>().ToListAsync();
		}

		public Task<TEntity> GetById<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : class
		{
			return this.DbContext.Set<TEntity>().FindAsync(id);
		}

		public void Dispose()
		{
			if (this.DbContext != null)
			{
				this.DbContext.SaveChanges();
				(this.DbContext as IDisposable).Dispose();
				this.DbContext = null;
			}
		}
	}
}
