using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyForum.BLL;
using MyForum.BLL.Interface;
using MyForum.JWT;
using MyForum.Model;

namespace MyForum.Api.Controllers
{
	[Route("/topic")]
	[ApiController]

	public class TopicController : BaseController
	{
		private readonly JWTSettings jwtSettings;
		private readonly ITopicService _topicService;
		private readonly ICollectService _collectService;
		private readonly IUserService _userService;
		private readonly ICommentService _commentService;
		private readonly IConfiguration _configuration;
		private IAccessTokenService _accessTokenService;

		public TopicController(ITopicService topicService
			, ICollectService collectService, IUserService userService
			, ICommentService commentService, IConfiguration configuration
			, IOptions<JWTSettings> options
			, IAccessTokenService accessTokenService):base(options,userService,accessTokenService)
		{
			_topicService = topicService;
			_collectService = collectService;
			_userService = userService;
			_commentService = commentService;
			_configuration = configuration;
			jwtSettings = options.Value;
			_accessTokenService = accessTokenService;
		}

		[HttpGet("detail")]
		public async Task<ActionResult> Detail(int id)
		{
			Dictionary<string,object> dic = new Dictionary<string,object>();
			Topic topic = await _topicService.FindAsync(id);
			if (topic == null)
			{
				return BadRequest("无此话题");
			}
			topic.View += 1;
			
			User user = await GetUserForTopicDetail();
			if(user != null && _collectService.FindByUserIdAndTopicId(user.Id, id) != null)
			{
				dic.Add("collect", true);
			} else
			{
				dic.Add("collect", false);
			}
			User topicUser = await _userService.FindAsync(topic.UserId);
			topic.User = topicUser;
			bool b = await _topicService.EditAsync(topic);
			if (!b)
				return BadRequest();
			List<Comment> comments = await _commentService.FindByTopicId(id);
			comments.ForEach(async comment =>
			{
				comment.User = await _userService.FindAsync(comment.UserId);
				if (comment.SecondComment != null)		{
					comment.SecondComment = await _commentService.FindAsync(comment.CommentId);
				}
			});
			dic.Add("topic", topic);
			dic.Add("comments", comments);
			return Ok(dic);
		}
		[HttpPost("create")]
		[Authorize]
		public async Task<ActionResult> Create([FromForm] string title, 
			[FromForm] string url, [FromForm] string content, [FromForm] string tab)
		{
			User user = await GetUser();
			Topic topic = new Topic { Title = title, Url = url, Content = content, Tab = tab,InTime = DateTime.Now };
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
				return Ok(topic.Id);
			} else
			{
				return BadRequest("创建话题失败");
			}
		}
		[HttpPost("update")]
		[Authorize]
		public async Task<ActionResult> Update([FromForm]int id, [FromForm] string title, [FromForm] string content, [FromForm] string tab)
		{
			User user = await GetUser();
			Topic oldTopic = await _topicService.FindAsync(id);
			if(oldTopic != null)
			{
				return BadRequest("话题不存在");
			}
			if(user.Id != oldTopic.UserId || !_configuration.GetSection("site").GetSection("admin").Value!.Contains(user.Username))
			{
				return BadRequest("无权更改话题");
			}
			oldTopic.Title = title;
			oldTopic.Content = content;
			oldTopic.Tab = tab;
			oldTopic.ModifyTime = DateTime.Now;
			bool b = await _topicService.EditAsync(oldTopic);
			if (b)
			{
				return Ok();
			} else
			{
				return BadRequest("更新话题失败");
			}
		}

		[HttpPost("delete")]
		[Authorize]
		public async Task<ActionResult> Delete([FromForm] int id)
		{
			User user = await GetUser();
			Topic topic = await _topicService.FindAsync(id);
			if(user.Id != topic.UserId || !_configuration.GetSection("site").GetSection("admin").Value!.Contains(user.Username))
			{
				return BadRequest("无权删除话题");
			}
			bool b = await _topicService.DeleteAsync(id);
			if (b)
			{

				return Ok();
			} else
			{
				return BadRequest("删除话题失败");
			}
		}

		[HttpGet("good")]
		[Authorize]
		public async Task<ActionResult> Good([FromForm] int id)
		{
			User user = await GetUser();
			Topic oldTopic = await _topicService.FindAsync(id);
			if (oldTopic == null)
				return BadRequest("没有此话题");
			if (!_configuration.GetSection("site").GetSection("admin").Value!.Contains(user.Username))
			{
				return BadRequest("没有权限");
			}
			User topicUser = await _userService.FindAsync(oldTopic.UserId);
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
				return Ok();
			} else
			{
				return BadRequest("话题加精（取消）失败");
			}
		}

		[HttpGet("top")]
		[Authorize]
		public async Task<ActionResult> Top([FromForm] int id)
		{
			User user = await GetUser();
			Topic topic = await _topicService.FindAsync(id);
			if (topic == null)
				return BadRequest("不存在");
			if (!_configuration.GetSection("site").GetSection("admin").Value!.Contains(user.Username))
			{
				return BadRequest("没有权限");
			}
			topic.Top = !topic.Top;
			bool b = await _topicService.EditAsync(topic);
			if (b)
			{
				return Ok();
			} else
			{
				return BadRequest("话题置顶（取消）失败");
			}
		}
	}
}
