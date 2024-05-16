using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Interface
{
	public interface IAccessTokenRepository : IBaseRepository<AccessToken>
	{
		Task<AccessToken> FindByToken(string token);
		Task<bool> DeleteByUserId(int id);
		Task<bool> Save(AccessToken entity);
		Task<AccessToken> FindByUserId(int userId);
	}
}
