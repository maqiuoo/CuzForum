using Microsoft.EntityFrameworkCore;
using MyForum.BLL.Interface;
using MyForum.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BLL
{
	public class BaseService<T> : IBaseService<T> where T : class,new()
	{
		protected IBaseRepository<T> baseRepository;

		public Task<bool> CreateAsync(T entity)
		{
			return baseRepository.CreateAsync(entity);
		}

		public virtual Task<bool> DeleteAsync(int id)
		{
			return baseRepository.DeleteAsync(id);
		}

		public Task<bool> EditAsync(T entity)
		{
			return baseRepository.EditAsync(entity);
		}

		public Task<T> FindAsync(int id)
		{
			return baseRepository.FindAsync(id);
		}

		public Task<T> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> func)
		{
			return baseRepository.FindAsync(func);
		}

		public Task<List<T>> QueryAsync()
		{
			throw new NotImplementedException();
		}

		public Task<List<T>> QueryAsync(System.Linq.Expressions.Expression<Func<T, bool>> func)
		{
			throw new NotImplementedException();
		}
	}
}
