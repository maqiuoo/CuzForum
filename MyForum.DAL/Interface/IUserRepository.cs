using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Interface
{
	public interface IUserRepository : IBaseRepository<User>
	{
		List<User> FindTop100(int size);
		Task<User> FindByUserName(string username);
		Task<User> FindByUserEmail(string email);
	}
}
