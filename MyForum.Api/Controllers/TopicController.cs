using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyForum.BLL.Interface;
using MyForum.Model;

namespace MyForum.Api.Controllers
{
	[Route("/topic")]
	[ApiController]
	public class TopicController : ControllerBase
	{
		private readonly ITopicService _topicService;

		public TopicController(ITopicService topicService)
		{
			_topicService = topicService;
		}

		[HttpGet("detail")]
		public async Task<ActionResult> Detail(int id)
		{
			Topic topic = await _topicService.FindAsync(id);
			if (topic != null)
			{
				return Ok(topic);
			} else
			{
				return BadRequest("无此话题");
			}
		}
		[HttpPost("create")]
		public async Task<ActionResult> Create([FromForm] string title, 
			[FromForm] string url, [FromForm] string content, [FromForm] string tab)
		{
			Topic topic = new Topic { Title = title, Url = url, Content = content, Tab = tab };
			bool b = await _topicService.CreateAsync(topic);
			if(b)
			{
				return Ok(topic.Id);
			} else
			{
				return BadRequest("创建话题失败");
			}
		}
		[HttpPost("update")]
		public async Task<ActionResult> Update([FromForm]int id, [FromForm] string title, [FromForm] string content, [FromForm] string tab)
		{
			Topic oldTopic = await _topicService.FindAsync(id);
			oldTopic.Title = title;
			oldTopic.Content = content;
			oldTopic.Tab = tab;
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
		public async Task<ActionResult> Delete([FromForm] int id)
		{
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
		public async Task<ActionResult> Good([FromForm] int id)
		{
			Topic oldTopic = await _topicService.FindAsync(id);
			oldTopic.Good = !oldTopic.Good;
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
		public async Task<ActionResult> Top([FromForm] int id)
		{
			Topic oldTopic = await _topicService.FindAsync(id);
			oldTopic.Top = !oldTopic.Top;
			bool b = await _topicService.EditAsync(oldTopic);
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
