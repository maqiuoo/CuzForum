using HtmlAgilityPack;
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
	[Route("/comment")]
	[ApiController]
	[Authorize]
	public class CommentController : BaseController
	{
		private readonly ICommentService _commentService;
		private readonly ITopicService topicService;
		private readonly INotificationService notificationService;

		public CommentController(IOptions<JWTSettings> options, IUserService userService, IAccessTokenService accessTokenService, IConfiguration configuration, ICommentService commentService, ITopicService topicService, INotificationService notificationService) : base(options, userService, accessTokenService, configuration)
		{
			_commentService = commentService;
			this.topicService = topicService;
			this.notificationService = notificationService;
		}

		[HttpPost("create")]
		public async Task<APIResult> Create([FromForm]int topicId, [FromForm] int? commentId, [FromForm] string content)
		{
			User user = await GetUser();
			var topic = await topicService.FindAsync(topicId);
			if ( topic == null)
			{
				return APIResultHelper.Error("话题不存在");
			}
			content = content.Replace("\\n", "<br/>");
			var targetUserId = topic.UserId.Value;
			var action = "COMMENT";
			if (commentId.HasValue)
			{
				Comment _comment = await _commentService.FindAsync(commentId.Value);
				if(_comment == null )
				{
					return APIResultHelper.Error("回复的评论不存在");
				}
				if(topic.UserId != _comment.UserId)
				{
					//创建通知 给话题作者发通知
					await notificationService.Create(topicId,user.Id,targetUserId,action);
				}
				targetUserId = _comment.UserId.Value;
				action = "REPLY";
				await notificationService.Create(topicId,user.Id,targetUserId,action);
			} else
			{
				await notificationService.Create(topicId,user.Id,targetUserId,action);
			}
			Comment comment = new Comment() { Content= content,TopicId=topicId,InTime = DateTime.Now,UserId=user.Id,User=user,Topic=topic};
			if(commentId != null)
			{
				comment.CommentId = (int)commentId;
			}
			bool b = await _commentService.CreateAsync(comment);
			if (b)
			{
				user.Score += int.Parse(_configuration.GetSection("site").GetSection("createCommentScore").Value!);
				await _userService.EditAsync(user);
				comment.User = user;
				topic.CommentCount++;
				await topicService.EditAsync(topic);
				return APIResultHelper.Success(comment);
			} else
			{
				return APIResultHelper.Error("创建失败");
			}
		}

		[HttpPost("update")]
		public async Task<APIResult> Update([FromForm] int id, [FromForm] string content)
		{
			User user = await GetUser();
			Comment comment = await _commentService.FindAsync(id);
			if (comment == null)
			{
				return APIResultHelper.Error("没有这条评论");
			}
			var sections = _configuration.GetSection("site").GetSection("admin").GetChildren().ToList();
			List<string> admins = new List<string>();
			foreach (var section in sections)
			{
				admins.Add(section.Value);
			}
			if (user.Id != comment.UserId || (admins.Count > 0 && !admins.Contains(user.Username)))
			{
				return APIResultHelper.Error("不能修改别人的评论");
			}
			HtmlDocument htmlDoc = new HtmlDocument();
			htmlDoc.LoadHtml(comment.Content);
			// 基于HtmlAgilityPack来获取无标签的纯文本
			string cleanContent = HtmlEntity.DeEntitize(htmlDoc.DocumentNode.InnerText);
			// 设置评论的内容为清理后的文本
			comment.Content = cleanContent;
			bool b = await _commentService.EditAsync(comment);
			if (b)
			{
				return APIResultHelper.Success();
			} else
			{
				return APIResultHelper.Error("更新失败");
			}
		}
		[HttpGet("delete")]
		public async Task<APIResult> Delete(int id)
		{
			User user = await GetUser();
			Comment comment = await _commentService.FindAsync(id);
			if (comment == null)
			{
				return APIResultHelper.Error("没有这条评论");
			}
			var sections = _configuration.GetSection("site").GetSection("admin").GetChildren().ToList();
			List<string> admins = new List<string>();
			foreach (var section in sections)
			{
				admins.Add(section.Value);
			}
			if (user.Id != comment.UserId || (admins.Count > 0 && !admins.Contains(user.Username)))
			{
				return APIResultHelper.Error("不能删除别人的评论");
			}
			User commentUser = await _userService.FindAsync(comment.UserId.Value);
			Topic topic = await topicService.FindAsync(comment.TopicId.Value);
			topic.CommentCount -= 1;
			await topicService.EditAsync(topic);
			bool b = await _commentService.DeleteAsync(id);
			if (b)
			{
				commentUser.Score -= int.Parse(_configuration.GetSection("site").GetSection("createCommentScore").Value!);
				await _userService.EditAsync(commentUser);
				return APIResultHelper.Success();
			} else
			{
				return APIResultHelper.Error("删除失败");
			}
		}
	}
}
