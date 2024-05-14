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

		public HomeController(ITopicService topicService, IUserService userService)
		{
			_topicService = topicService;
			_userService = userService;
		}

		[HttpGet]
		public async Task<ActionResult> Home(int pageNo,int pageSize,string tab)
		{
			var topics = await _topicService.FindAll(pageNo, pageSize, tab);
			return Ok(topics);
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
