using MyForum.BLL.Interface;
using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BLL
{
	public class UserService : BaseService<User>, IUserService
	{
		private readonly IUserRepository userRepository;

		public UserService(IUserRepository userRepository)
		{
			base.baseRepository = userRepository;
			this.userRepository = userRepository;
		}
	}
}
