using Microsoft.AspNetCore.Authorization;
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
	[Route("/topic")]
	[ApiController]
	[Authorize]
	public class TopicController : BaseController
	{
		public TopicController(IOptions<JWTSettings> options, IUserService userService, IAccessTokenService accessTokenService, IConfiguration configuration, ITopicService topicService, ICollectService collectService, ICommentService commentService) : base(options, userService, accessTokenService, configuration)
		{
			_topicService = topicService;
			_collectService = collectService;
			_commentService = commentService;
		}

		//private readonly JWTSettings jwtSettings;
		private readonly ITopicService _topicService;
		private readonly ICollectService _collectService;
		//private readonly IUserService _userService;
		private readonly ICommentService _commentService;
		//private readonly IConfiguration _configuration;
		//private IAccessTokenService _accessTokenService;

		//public TopicController(ITopicService topicService
		//	, ICollectService collectService, IUserService userService
		//	, ICommentService commentService, IConfiguration configuration
		//	, IOptions<JWTSettings> options
		//	, IAccessTokenService accessTokenService):base(options,userService,accessTokenService,)
		//{
		//	_topicService = topicService;
		//	_collectService = collectService;
		//	_userService = userService;
		//	_commentService = commentService;
		//	_configuration = configuration;
		//	jwtSettings = options.Value;
		//	_accessTokenService = accessTokenService;
		//}

		[HttpGet("detail")]
		public async Task<APIResult> Detail(int id)
		{
			Dictionary<string,object> dic = new Dictionary<string,object>();
			Topic topic = await _topicService.FindAsync(id);
			if (topic == null)
			{
				return APIResultHelper.Error("无此话题");
			}
			topic.View += 1;
			
			User user = await GetUser();
			if(user != null && await _collectService.FindByUserIdAndTopicId(user.Id, id) != null)
			{
				dic.Add("collect", true);
			} else
			{
				dic.Add("collect", false);
			}
			User topicUser = await _userService.FindAsync(topic.UserId.Value);
			topic.User = topicUser;
			bool b = await _topicService.EditAsync(topic);
			if (!b)
				return APIResultHelper.Error("更新失败");
			List<Comment> comments = await _commentService.FindByTopicId(id);
			foreach (var comment in comments)
			{
				comment.User = await _userService.FindAsync(comment.UserId.Value);
				if (comment.SecondComment != null)
				{
					comment.SecondComment = await _commentService.FindAsync(comment.CommentId.Value);
				}
			}
			dic.Add("topic", topic);
			dic.Add("comments", comments);
			return APIResultHelper.Success(dic);
		}
		[HttpPost("create")]
		[Authorize]
		public async Task<APIResult> Create([FromForm] string title, 
			[FromForm] string? url, [FromForm] string content, [FromForm] string tab)
		{
			User user = await GetUser();
			Topic topic = new Topic { Title = title, Url = url, Content = content, Tab = tab,InTime = DateTime.Now,User = user };
			topic.UserId = user.Id;
			topic.CommentCount = 0;
			topic.CollectCount = 0;
			topic.View = 0;
			topic.Good = false;
			topic.Top = false;
			bool b = await _topicService.CreateAsync(topic);
			if(b)
			{
				user.Score += int.Parse(_configuration.GetSection("site").GetSection("createTopicScore").Value!);
				await _userService.EditAsync(user);
				return APIResultHelper.Success(topic.Id);
			} else
			{
				return APIResultHelper.Error("创建话题失败");
			}
		}
		[HttpPost("update")]
		[Authorize]
		public async Task<APIResult> Update([FromForm]int id, [FromForm] string title, [FromForm] string content, [FromForm] string tab)
		{
			User user = await GetUser();
			Topic oldTopic = await _topicService.FindAsync(id);
			if(oldTopic == null)
			{
				return APIResultHelper.Error("话题不存在");
			}
			var sections = _configuration.GetSection("site").GetSection("admin").GetChildren().ToList();
			List<string> admins = new List<string>();
			foreach(var section in sections)
			{
				admins.Add(section.Value);
			}
			if (user.Id != oldTopic.UserId||(admins.Count > 0 && !admins.Contains(user.Username)))
			{
				return APIResultHelper.Error("无权更改话题");
			}
			oldTopic.Title = title;
			oldTopic.Content = content;
			oldTopic.Tab = tab;
			oldTopic.ModifyTime = DateTime.Now;
			bool b = await _topicService.EditAsync(oldTopic);
			if (b)
			{
				return APIResultHelper.Success();
			} else
			{
				return APIResultHelper.Error("更新话题失败");
			}
		}

		[HttpGet("delete")]
		[Authorize]
		public async Task<APIResult> Delete(int id)
		{
			User user = await GetUser();
			Topic topic = await _topicService.FindAsync(id);
			if(topic == null)
			{
				return APIResultHelper.Error("话题不存在");
			}
			var sections = _configuration.GetSection("site").GetSection("admin").GetChildren().ToList();
			List<string> admins = new List<string>();
			foreach (var section in sections)
			{
				admins.Add(section.Value);
			}
			if (user.Id != topic.UserId || (admins.Count > 0 && !admins.Contains(user.Username)))
			{
				return APIResultHelper.Error("无权删除话题");
			}
			try
			{
				await _collectService.DeleteByTopicId(id);
				await _commentService.DeleteByTopicId(id);
				await _topicService.DeleteAsync(id);
			}catch(Exception ex)
			{
				return APIResultHelper.Error("删除话题失败");
			}		
			return APIResultHelper.Success();
		}

		[HttpGet("good")]
		[Authorize]
		public async Task<APIResult> Good(int id)
		{
			User user = await GetUser();
			Topic oldTopic = await _topicService.FindAsync(id);
			if (oldTopic == null)
				return APIResultHelper.Error("没有此话题");
			var sections = _configuration.GetSection("site").GetSection("admin").GetChildren().ToList();
			List<string> admins = new List<string>();
			foreach (var section in sections)
			{
				admins.Add(section.Value);
			}
			if (user.Id != oldTopic.UserId || (admins.Count > 0 && !admins.Contains(user.Username)))
			{
				return APIResultHelper.Error("无权");
			}
			User topicUser = await _userService.FindAsync(oldTopic.UserId.Value);
			oldTopic.Good = !oldTopic.Good;
			if ((bool)oldTopic.Good!)
			{
				topicUser.Score += int.Parse(_configuration.GetSection("site").GetSection("goodTopicScore").Value!);
			} else
			{
				topicUser.Score -= int.Parse(_configuration.GetSection("site").GetSection("goodTopicScore").Value!);
			}
			await _userService.EditAsync(topicUser);
			bool b = await _topicService.EditAsync(oldTopic);
			if (b)
			{
				return APIResultHelper.Success();
			} else
			{
				return APIResultHelper.Error("话题加精（取消）失败");
			}
		}

		[HttpGet("top")]
		[Authorize]
		public async Task<APIResult> Top(int id)
		{
			User user = await GetUser();
			Topic topic = await _topicService.FindAsync(id);
			if (topic == null)
				return APIResultHelper.Error("不存在");
			var sections = _configuration.GetSection("site").GetSection("admin").GetChildren().ToList();
			List<string> admins = new List<string>();
			foreach (var section in sections)
			{
				admins.Add(section.Value);
			}
			if (user.Id != topic.UserId || (admins.Count > 0 && !admins.Contains(user.Username)))
			{
				return APIResultHelper.Error("无权");
			}
			topic.Top = !topic.Top;
			bool b = await _topicService.EditAsync(topic);
			if (b)
			{
				return APIResultHelper.Success();
			} else
			{
				return APIResultHelper.Error("话题置顶（取消）失败");
			}
		}
	}
}
