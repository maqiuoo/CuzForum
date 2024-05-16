using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BLL.Interface
{
	public interface IAccessTokenService :IBaseService<AccessToken>
	{
		Task<AccessToken> FindByToken(string token,string secKey);
		Task<bool> DeleteByUserId(int id);
		Task<bool> Save(AccessToken entity);
		Task<AccessToken> FindByUserId(int userId, string secKey);
	}
}
