using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyForum.BLL;
using MyForum.BLL.Interface;
using MyForum.JWT;
using MyForum.JWT.APIResultFile;
using MyForum.Model;

namespace MyForum.Api.Controllers
{
	[Route("/")]
	[ApiController]
	public class NotificationController : BaseController
	{
		private readonly INotificationService notificationService;
		private readonly ITopicService topicService;
		public NotificationController(IOptions<JWTSettings> options, IUserService userService,
			IAccessTokenService accessTokenService, INotificationService notificationService, ITopicService topicService,IConfiguration configuration) : base(options, userService, accessTokenService,configuration)
		{
			this.notificationService = notificationService;
			this.topicService = topicService;
		}
		[HttpGet]
		[Route("list")]
		public async Task<APIResult> List(int pageNo = 1)
		{
			User user = await GetUser();
			int pageSize = int.Parse(_configuration.GetSection("site").GetSection("pageSize").Value!);
			List<Notification> page = await notificationService.FindByUserId(user.Id, pageNo,pageSize);
			foreach (var notification in page)
			{
				var topic = await topicService.FindAsync(notification.TopicId.Value);
				var _user = await _userService.FindAsync(notification.UserId.Value);
				notification.Topic = topic;
				notification.User = user;
			}
			return APIResultHelper.Success(page);
		}
		[HttpGet]
		[Route("notRead")]
		public async Task<APIResult> NotRead()
		{
			User user = await GetUser();
			long count = await notificationService.CountNotRead(user.Id);
			return APIResultHelper.Success(count);
		}
	}
}
