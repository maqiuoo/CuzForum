using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyForum.BLL.Interface;
using MyForum.JWT;
using MyForum.JWT.APIResultFile;
using MyForum.Model;

namespace MyForum.Api.Controllers
{
	[Route("/collect")]
	[ApiController]
	public class CollectController : BaseController
	{
		private readonly ICollectService collectService;
		private readonly ITopicService topicService;
		private readonly INotificationService notificationService;
		public CollectController(IOptions<JWTSettings> options, IUserService userService, IAccessTokenService accessTokenService, IConfiguration configuration, ICollectService collectService, ITopicService topicService, INotificationService notificationService) : base(options, userService, accessTokenService, configuration)
		{
			this.collectService = collectService;
			this.topicService = topicService;
			this.notificationService = notificationService;
		}
		[HttpPost]
		[Route("save")]
		public async Task<APIResult> Save([FromBody]int topicId)
		{
			User user = await GetUser();
			Topic topic = await topicService.FindAsync(topicId);
			if(topic == null)
			{
				return APIResultHelper.Error("收藏的话题不存在");
			}
			Collect collect = await collectService.FindByUserIdAndTopicId(user.Id, topicId);	
			if(collect != null)
			{
				return APIResultHelper.Error("已经收藏了这个话题");
			}
			collect = new Collect() { TopicId =  topicId,UserId = user.Id,InTime = DateTime.Now,Topic=topic,User=user};
			await collectService.CreateAsync(collect);
			topic.CollectCount += 1;
			await topicService.EditAsync(topic);
			bool b = await notificationService.Create(topicId, user.Id, topic.UserId.Value, "COLLECT");
			if (b)
			{
				return APIResultHelper.Success();
			} else
			{
				return APIResultHelper.Error("出现错误");
			}
		}
		[HttpPost]
		[Route("delete")]
		public async Task<APIResult> Delete(int topicId)
		{
			User user = await GetUser();
			Topic topic = await topicService.FindAsync(topicId);
			if (topic == null)
			{
				return APIResultHelper.Error("要取消收藏的话题不存在");
			}
			Collect collect = await collectService.FindByUserIdAndTopicId(user.Id,topicId);
			if(collect == null)
			{
				return APIResultHelper.Error("还没有收藏这个话题");
			}
			topic.CollectCount -= 1;
			await topicService.EditAsync(topic);
			bool b = await collectService.DeleteAsync(collect.Id);
			if (b)
			{
				return APIResultHelper.Success();
			} else
			{
				return APIResultHelper.Error("删除出现错误");
			}
		}
	}
}
