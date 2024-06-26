using Microsoft.EntityFrameworkCore;
using MyForum.DAL.Interface;
using MyForum.Model;
using MyForum.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ModelDbContext dbctx) : base(dbctx)
        {
        }

        public Task<User> FindByUserEmail(string email)
        {
            var user = dbctx.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public Task<User> FindByUserName(string username)
        {
            var user = dbctx.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public List<User> FindTop100(int size)
        {
            var users = dbctx.Users.Take(size).ToList();
            return users;
        }
    }
}
