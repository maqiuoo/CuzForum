using Azure;
using Microsoft.EntityFrameworkCore;
using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class,IEntity,new() 
	{
		protected readonly ModelDbContext dbctx;

		public BaseRepository(ModelDbContext dbctx = null)
		{
			this.dbctx = dbctx;
		}

		public async Task<bool> CreateAsync(T entity)
		{
			await dbctx.Set<T>().AddAsync(entity);
			return await dbctx.SaveChangesAsync() > 0;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			T entity = await dbctx.Set<T>().FirstAsync(t => t.Id == id);
			if(entity == null)
			{
				return false;
			} else
			{
				dbctx.Set<T>().Remove(entity);
			}
			return await dbctx.SaveChangesAsync() > 0;
		}

		public async Task<bool> EditAsync(T entity)
		{
			T oldEntity = await dbctx.Set<T>().FirstAsync(t => t.Id == entity.Id);
			if (oldEntity != null)
			{
				var entityProperties = typeof(T).GetProperties();
				foreach (var property in entityProperties)
				{
					if (property.CanWrite)
					{
						var newValue = property.GetValue(entity);
						property.SetValue(oldEntity, newValue);
					}
				}
				await dbctx.SaveChangesAsync();
				return true;
			} else
			{
				return false;
			}
		}

		public async Task<T> FindAsync(int id)
		{
			T entity = await dbctx.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
			if(entity != null)
				return entity;
			return null;
		}

		public async Task<T> FindAsync(Expression<Func<T, bool>> func)
		{
			return await dbctx.Set<T>().FirstOrDefaultAsync(func);
		}

		public Task<List<T>> QueryAsync()
		{
			throw new NotImplementedException();
		}

		public Task<List<T>> QueryAsync(Expression<Func<T, bool>> func)
		{
			throw new NotImplementedException();
		}

	}
}
