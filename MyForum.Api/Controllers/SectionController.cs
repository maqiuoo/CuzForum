using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyForum.BLL.Interface;
using MyForum.JWT;
using System.Text.Json;

namespace MyForum.Api.Controllers
{
	[Route("/")]
	[ApiController]
	public class SectionController : BaseController
	{
		public SectionController(IOptions<JWTSettings> options, IUserService userService, IAccessTokenService accessTokenService, IConfiguration configuration) : base(options, userService, accessTokenService, configuration)
		{
		}

		[HttpGet("/section")]
		public ActionResult Sections()
		{
			var configuration = _configuration.GetSection("site").GetSection("sections").GetChildren();
			Dictionary<string,string> res = new Dictionary<string, string>();
			foreach(var section in configuration)
			{
				res.Add(section.GetSection("name").Value!,section.GetSection("value").Value!);
			}
			return Ok(res);
		}
	}
}
