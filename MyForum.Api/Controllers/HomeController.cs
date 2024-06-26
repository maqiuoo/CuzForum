using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyForum.BLL.Interface;
using MyForum.JWT;
using MyForum.JWT.APIResultFile;
using MyForum.Model;

namespace MyForum.Api.Controllers
{
	[Route("/")]
	[ApiController]
	public class HomeController : BaseController
	{
		private readonly ITopicService topicService;
		private readonly ICommentService commentService;
		private readonly ICollectService collectService;
		public HomeController(IOptions<JWTSettings> options, IUserService userService, IAccessTokenService accessTokenService, IConfiguration configuration, ITopicService topicService, ICommentService commentService, ICollectService collectService) : base(options, userService, accessTokenService, configuration)
		{
			this.topicService = topicService;
			this.commentService = commentService;
			this.collectService = collectService;
		}

		[HttpGet]
		public async Task<APIResult> Home(string? tab, int pageNo = 1)
		{
			int pageSize = int.Parse(_configuration.GetSection("site").GetSection("pageSize").Value!);
			int allCount = await topicService.GetAll();
			if (tab == null || tab.Equals("all"))
			{
				var topics = await topicService.FindAll(pageNo, pageSize, string.Empty);
				//所有话题数量等于获取话题数量或获取话题数量小于该页话题数量
				if (topics.Count == allCount || topics.Count < pageSize)
				{
					return APIResultHelper.Success(new { last = true, content = topics });
				} else
				{
					return APIResultHelper.Success(new { last = false, content = topics });
				}
			} else
			{
				var topics = await topicService.FindAll(pageNo, pageSize, tab);
				if (topics.Count == allCount || topics.Count < pageSize)
				{
					return APIResultHelper.Success(new { last = true, content = topics });
				} else
				{
					return APIResultHelper.Success(new { last = false, content = topics });
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
		[Authorize]
		[HttpPost("upload")]
		public async Task<APIResult> Upload(IFormFile file)
		{
			if(file == null || file.Length == 0)
			{
				return APIResultHelper.Error("请上传文件");
			}
			string base64String;
			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				byte[] fileBytes = memoryStream.ToArray();
				base64String = Convert.ToBase64String(fileBytes);
			}
			User user = await GetUser();
			user.Avatar = base64String;
			await _userService.EditAsync(user);
			return APIResultHelper.Success(user);
		}
	}
}
