using Microsoft.EntityFrameworkCore;
using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(ModelDbContext dbctx) : base(dbctx)
		{
		}
	}
}
