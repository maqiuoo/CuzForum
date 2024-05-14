using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BLL.Interface
{
	public interface IUserService : IBaseService<User>
	{
		List<User> FindTop100(int size);
	}
}
