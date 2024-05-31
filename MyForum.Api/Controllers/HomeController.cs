using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyForum.BLL.Interface;
using MyForum.Model;

namespace MyForum.Api.Controllers
{
	[Route("/")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		private readonly ITopicService _topicService;
		private readonly IUserService _userService;
		private readonly IConfiguration configuration;

		public HomeController(ITopicService topicService, IUserService userService, IConfiguration configuration)
		{
			_topicService = topicService;
			_userService = userService;
			this.configuration = configuration;
		}

		[HttpGet]
		public async Task<ActionResult> Home(string? tab,int pageNo = 1)
		{
			int pageSize = int.Parse(configuration.GetSection("site").GetSection("pageSize").Value!);
			if (tab == null)
			{
				var t = await _topicService.FindAll(pageNo, pageSize, string.Empty);
				return Ok(t);
			} else
			{
				var topics = await _topicService.FindAll(pageNo, pageSize, tab);
				return Ok(topics);
			}
			
		}
		[HttpGet("top100")]
		public ActionResult Top100(int size = 7)
		{
			if (size > 100)
				return BadRequest("size不能超过100");
			var users = _userService.FindTop100(size);
			return Ok(users);
		}
	}
}
