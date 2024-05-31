using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyForum.BLL.Interface;
using MyForum.JWT.APIResultFile;
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
		public async Task<APIResult> Home(string? tab,int pageNo = 1)
		{
			int pageSize = int.Parse(configuration.GetSection("site").GetSection("pageSize").Value!);
			int allCount = await _topicService.GetAll();
			if (tab == null || tab.Equals("all"))
			{
				var t = await _topicService.FindAll(pageNo, pageSize, string.Empty);
				if (t.Count < allCount)
				{
					return APIResultHelper.Success(new { last = false, content = t });
				} else
				{
					return APIResultHelper.Success(new { last = true, content = t });
				}
			} else
			{
				var topics = await _topicService.FindAll(pageNo, pageSize, tab);
				if (topics.Count < allCount)
				{
					return APIResultHelper.Success(new { last = false, content = topics });
				} else
				{
					return APIResultHelper.Success(new { last = true, content = topics });
				}
			}
			
		}
		[HttpGet("top100")]
		public APIResult Top100(int size = 7)
		{
			if (size > 100)
				return APIResultHelper.Error("size不能超过100");
			var users = _userService.FindTop100(size);
			return APIResultHelper.Success(users);
		}
	}
}
