using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MyForum.Api.Utility;
using MyForum.BLL;
using MyForum.BLL.Interface;
using MyForum.JWT;
using MyForum.Model;

namespace MyForum.Api.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected readonly JWTSettings jwtSettings;
		protected readonly IUserService _userService;
		protected readonly IAccessTokenService _accessTokenService;
		protected readonly IConfiguration _configuration;

		public BaseController(IOptions<JWTSettings> options, IUserService userService, IAccessTokenService accessTokenService, IConfiguration configuration)
		{
			jwtSettings = options.Value;
			_userService = userService;
			_accessTokenService = accessTokenService;
			_configuration = configuration;
		}

		protected async Task<User> GetUserForTopicDetail()
		{
			// 从请求头中获取token
			
			var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
			if (authHeader != null && authHeader.StartsWith("Bearer "))
			{
				var token = authHeader.Substring("Bearer ".Length);
				var accessToken = await _accessTokenService.FindByToken(token, jwtSettings.SecKey);
				if (accessToken != null)
				{
					var user = await _userService.FindAsync(accessToken.UserId);
					if (user != null)
					{
						return user; // 返回用户信息
					}
				}
			}

			return null; // 如果没有找到有效的token或用户，返回
		}
		private async Task<string> GetToken()
		{
			var authHeader = Request.Headers["Authorization"].ToString();
			if (string.IsNullOrEmpty(authHeader))
			{
				throw new Exception("授权头部不可为空");
			}
			var token = string.Empty;
			if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
			{
				// 从授权头部中去掉"Bearer "这个前缀，剩下的部分就是JWT令牌
				token = authHeader.Substring("Bearer ".Length).Trim();
			}
			// 使用accessTokenService来找到和验证AccessToken
			var accessToken = await _accessTokenService.FindByToken(token, jwtSettings.SecKey);
			if (accessToken == null)
			{
				throw new Exception("您的Token已无效，请重新登录获取");
			}
			return token;
		}
		protected async Task<User> GetUser()
		{
			string secKey = jwtSettings.SecKey;
			int exp = jwtSettings.ExpireSeconds;
			JwtTokenUtil jwtTokenUtil = new JwtTokenUtil(secKey,exp);
			User user = await _userService.FindByUserName(jwtTokenUtil.GetUsernameFromToken(await GetToken()));
			return user;
				
		}
	}
}
