using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyForum.BLL.Interface;
using MyForum.JWT;
using MyForum.Model;

namespace MyForum.Api.Controllers
{
	[Route("/")]
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
		public async Task<ActionResult> Save(int topicId)
		{
			User user = await GetUser();
			Topic topic = await topicService.FindAsync(topicId);
			if(topic == null)
			{
				return BadRequest("收藏的话题不存在");
			}
			Collect collect = await collectService.FindByUserIdAndTopicId(user.Id, topicId);	
			if(collect != null)
			{
				return BadRequest("已经收藏了这个话题");
			}
			collect = new Collect() { TopicId =  topicId,UserId = user.Id,InTime = DateTime.Now };
			await collectService.CreateAsync(collect);
			topic.CollectCount += 1;
			await topicService.EditAsync(topic);
			bool b = await notificationService.Create(topicId, user.Id, topic.UserId, "COLLECT");
			if (b)
			{
				return Ok();
			} else
			{
				return BadRequest("出现错误");
			}
		}
		[HttpPost]
		[Route("delete")]
		public async Task<ActionResult> Delete(int topicId)
		{
			User user = await GetUser();
			Topic topic = await topicService.FindAsync(topicId);
			if (topic == null)
			{
				return BadRequest("要取消收藏的话题不存在");
			}
			Collect collect = await collectService.FindByUserIdAndTopicId(user.Id,topicId);
			if(collect == null)
			{
				return BadRequest("还没有收藏这个话题");
			}
			topic.CollectCount -= 1;
			await topicService.EditAsync(topic);
			bool b = await collectService.DeleteAsync(collect.Id);
			if (b)
			{
				return Ok();
			} else
			{
				return BadRequest("删除出现错误");
			}
		}
	}
}
