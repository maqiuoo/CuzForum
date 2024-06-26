using Ganss.Xss;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyForum.BLL;
using MyForum.BLL.Interface;
using MyForum.JWT;
using MyForum.JWT.APIResultFile;
using MyForum.JWT.Utility;
using MyForum.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyForum.Api.Controllers
{
	[Route("/user")]
	[ApiController]
	[EnableCors("MyPolicy")]
	public class AuthorizeController : BaseController
	{
		private readonly ITopicService topicService;
		private readonly ICommentService commentService;
		private readonly ICollectService collectService;
		public AuthorizeController(IOptions<JWTSettings> options, IUserService userService, IAccessTokenService accessTokenService, IConfiguration configuration, ITopicService topicService, ICommentService commentService, ICollectService collectService) : base(options, userService, accessTokenService, configuration)
		{
			this.topicService = topicService;
			this.commentService = commentService;
			this.collectService = collectService;
		}

		//public AuthorizeController(IUserService iUserService, IConfiguration configuration, IAccessTokenService accessTokenService)
		//{
		//	this.iUserService = iUserService;
		//	this.configuration = configuration;
		//	this.accessTokenService = accessTokenService;
		//}
		[HttpPost("login")]

		public async Task<APIResult> Login([FromForm] string username, [FromForm] string password)
		{
			string pwd = MD5Helper.MD5Encrypt32(password);
			var user = await _userService.FindAsync(c => c.Username == username && c.Password == pwd);
			if (user != null)
			{
				var claims = new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Username),
					new Claim("Id",user.Id.ToString()),
					new Claim("UserName",user.Username)
				};
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").GetSection("SecKey").Value!));
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
				await _accessTokenService.Save(accessToken);
				return APIResultHelper.Success(new {token = jwtToken,isAdmin = user.IsAdmin });
			} else
			{
				return APIResultHelper.Error("账号或密码错误");
			}
		}

		[HttpPost("register")]
		public async Task<APIResult> Register([FromForm] string username, [FromForm] string password)
		{
			string pwd = MD5Helper.MD5Encrypt32(password);
			var user = await _userService.FindAsync(c => c.Username == username);
			if (user == null)
			{
				User u = new User() { Username = username, Password = pwd, InTime = DateTime.Now,Score = 0 };
				//获取jwttoken
				var claims = new Claim[]
				{
					new Claim(ClaimTypes.Name, u.Username),
					new Claim("Id",u.Id.ToString()),
					new Claim("UserName",u.Username)
				};
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT").GetSection("SecKey").Value!));
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
				bool b = await _userService.CreateAsync(u);
				AccessToken accessToken = new AccessToken() { InTime = DateTime.Now, Token = jwtToken,UserId = u.Id };
				await _accessTokenService.Save(accessToken);
				Dictionary<string,object> dic = new Dictionary<string,object>();
				dic.Add("jwtToken", accessToken);
				if (b)
				{
					return APIResultHelper.Success(dic);
				} else
				{
					return APIResultHelper.Error("注册失败");
				}
			} else
			{
				return APIResultHelper.Error("账号已存在");
			}
		}

		[HttpGet("{username}")]
		public async Task<APIResult> Index(string username)
		{
			User user = await _userService.FindByUserName(username);
			return APIResultHelper.Success(user);
		}

		[HttpGet("{username}/topics")]
		public async Task<APIResult> Topics(string username,int pageSize, int pageNo = 1)
		{
			if(pageSize > int.Parse(_configuration.GetSection("site").GetSection("pageSize").Value!))
			{
				pageSize = int.Parse(_configuration.GetSection("site").GetSection("pageSize").Value!);
			}
			User user = await _userService.FindByUserName(username);
			if(user == null)
			{
				return APIResultHelper.Error("user不存在");
			}
			var page = await topicService.FindByUserId(user.Id,pageNo,pageSize);
			foreach (var topic in page)
			{
				topic.User = user;
			}
			return APIResultHelper.Success(page);
		}

		[HttpGet("{username}/comments")]
		public async Task<APIResult> Comments(string username,int pageSize, int pageNo = 1)
		{
			if (pageSize > int.Parse(_configuration.GetSection("site").GetSection("pageSize").Value!))
			{
				pageSize = int.Parse(_configuration.GetSection("site").GetSection("pageSize").Value!);
			}
			User user = await _userService.FindByUserName(username);
			if (user == null)
			{
				return APIResultHelper.Error("user不存在");
			}
			var page = await commentService.FindByUserId(user.Id, pageNo, pageSize);
			foreach(var comment in page)
			{
				comment.User = user;
				comment.Topic = await topicService.FindAsync(comment.TopicId.Value);
				if (comment.CommentId.HasValue)
				{
					comment.SecondComment = await commentService.FindAsync(comment.CommentId.Value);
				}
			}
			return APIResultHelper.Success(page);
		}

		[HttpGet("{username}/collects")]
		public async Task<APIResult> Collects(string username,int pageSize, int pageNo = 1)
		{
			if (pageSize > int.Parse(_configuration.GetSection("site").GetSection("pageSize").Value!))
			{
				pageSize = int.Parse(_configuration.GetSection("site").GetSection("pageSize").Value!);
			}
			User user = await _userService.FindByUserName(username);
			if (user == null)
			{
				return APIResultHelper.Error("user不存在");
			}
			var page = await collectService.FindByUserId(user.Id, pageNo, pageSize);
			foreach(var collect in page)
			{
				collect.User = user;
				collect.Topic = await topicService.FindAsync(collect.TopicId.Value);
			}
			return APIResultHelper.Success(page);
		}

		[HttpGet("settings/profile")]
		public async Task<APIResult> Profile()
		{
			User user = await GetUser();
			var sections = _configuration.GetSection("site").GetSection("admin").GetChildren().ToList();
			List<string> admins = new List<string>();
			foreach (var section in sections)
			{
				admins.Add(section.Value);
			}
			if (admins.Contains(user.Username))
			{
				user.IsAdmin = true;
			}
			return APIResultHelper.Success(user);
		}

		[HttpPost("settings/profile")]
		public async Task<APIResult> Profile([FromForm] string? email, [FromForm] string? website, [FromForm] string? bio, [FromForm] string? avatar)
		{
			var sanitizer = new HtmlSanitizer();
			sanitizer.AllowedTags.Clear(); // 确保没有允许的标签，类似于Whitelist.none()
			User user = await GetUser();
			if (!string.IsNullOrEmpty(email))
			{
				User _user = await _userService.FindByUserEmail(email);
				if(_user != null && !email.Equals(user.Email))
				{
					return APIResultHelper.Error("邮箱已被占用");
				}
				var cleanEmail = sanitizer.Sanitize(email);
				user.Email = cleanEmail;
			}
			if (!string.IsNullOrEmpty(website))
			{
				var cleanWebsite = sanitizer.Sanitize(website);
				user.Website = cleanWebsite;
			}
			if (!string.IsNullOrEmpty(bio))
			{
				var cleanBio = sanitizer.Sanitize(bio);
				user.Bio = cleanBio;
			}
			if (!string.IsNullOrEmpty(avatar))
			{
				var cleanAvatar = sanitizer.Sanitize(avatar);
				user.Avatar = cleanAvatar;
			}
			await _userService.EditAsync(user);
			return APIResultHelper.Success();
		}

		[HttpPost("settings/updatePassword")]
		public async Task<APIResult> UpdatePassword([FromForm] string rawPassword, [FromForm] string newPassword)
		{
			User user = await GetUser();
			if(string.IsNullOrEmpty(rawPassword) || string.IsNullOrEmpty(newPassword))
			{
				return APIResultHelper.Error("新密码或旧密码不能为空");
			}
			//原密码输入错误
			if (!user.Password.Equals(MD5Helper.MD5Encrypt32(rawPassword)))
			{
				return APIResultHelper.Error("原密码输入错误");
			} else
			{
				await _accessTokenService.DeleteByUserId(user.Id);
				return APIResultHelper.Success();
			}
		}
	}
}
