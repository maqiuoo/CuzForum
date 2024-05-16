using MyForum.BLL.Interface;
using MyForum.BLL.Utility;
using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BLL
{
	public class AccessTokenService : BaseService<AccessToken>, IAccessTokenService
	{
		private readonly IAccessTokenRepository accessTokenRepository;

		public AccessTokenService(IAccessTokenRepository accessTokenRepository)
		{
			base.baseRepository = accessTokenRepository;
			this.accessTokenRepository = accessTokenRepository;
		}
		public Task<bool> DeleteByUserId(int id)
		{
			return accessTokenRepository.DeleteByUserId(id);
		}

		public Task<AccessToken> FindByToken(string token, string secKey)
		{
			var t = accessTokenRepository.FindByToken(token);
			bool b = JwtTokenUtil.IsTokenExpired(token, secKey);
			if (b)//过期
			{
				DeleteByUserId(t.Id);
				return null;
			} else
			{
				return t;
			}
		}

		public async Task<AccessToken> FindByUserId(int userId, string secKey)
		{
			var t = await accessTokenRepository.FindByUserId(userId);
			bool b = JwtTokenUtil.IsTokenExpired(t.Token, secKey);
			if (b)//过期
			{
				await DeleteByUserId(t.Id);
				return null;
			} else
			{
				return t;
			}
		}

		public Task<bool> Save(AccessToken entity)
		{
			return accessTokenRepository.Save(entity);
		}
	}
}
