using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyForum.BLL.Interface;
using MyForum.JWT.APIResultFile;
using MyForum.JWT.Utility;
using MyForum.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyForum.Api.Controllers
{
	[Route("/")]
	[ApiController]
	[EnableCors("MyPolicy")]
	public class AuthorizeController : ControllerBase
	{
		private readonly IUserService iUserService;
		private readonly IAccessTokenService accessTokenService;
		private readonly IConfiguration configuration;

		public AuthorizeController(IUserService iUserService, IConfiguration configuration, IAccessTokenService accessTokenService)
		{
			this.iUserService = iUserService;
			this.configuration = configuration;
			this.accessTokenService = accessTokenService;
		}
		[HttpPost("login")]

		public async Task<APIResult> Login([FromForm] string username, [FromForm] string password)
		{
			string pwd = MD5Helper.MD5Encrypt32(password);
			var user = await iUserService.FindAsync(c => c.Username == username && c.Password == pwd);
			if (user != null)
			{
				var claims = new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Username),
					new Claim("Id",user.Id.ToString()),
					new Claim("UserName",user.Username)
				};
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT").GetSection("SecKey").Value!));
				//issuer代表颁发Token的Web应用程序，audience是Token的受理者
				var token = new JwtSecurityToken(
					issuer: "http://localhost:6060",
					audience: "http://localhost:5000",
					claims: claims,
					notBefore: DateTime.Now,
					expires: DateTime.Now.AddHours(1),
					signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
				);
				var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
				AccessToken accessToken = new AccessToken() { InTime=DateTime.Now,Token =  jwtToken };
				await accessTokenService.Save(accessToken);
				return APIResultHelper.Success(jwtToken);
			} else
			{
				return APIResultHelper.Error("账号或密码错误");
			}
		}

		[HttpPost("register")]
		public async Task<APIResult> Register([FromForm] string username, [FromForm] string password)
		{
			string pwd = MD5Helper.MD5Encrypt32(password);
			var user = await iUserService.FindAsync(c => c.Username == username);
			if (user == null)
			{
				User u = new User() { Username = username, Password = pwd, InTime = DateTime.Now };
				bool b = await iUserService.CreateAsync(u);
				if (b)
				{
					return APIResultHelper.Success(u);
				} else
				{
					return APIResultHelper.Error("注册失败");
				}
			} else
			{
				return APIResultHelper.Error("账号已存在");
			}
		}
	}
}
