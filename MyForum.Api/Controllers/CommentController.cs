using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyForum.BLL.Interface;
using MyForum.Model;

namespace MyForum.Api.Controllers
{
	[Route("/comment")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentService _commentService;

		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpPost("create")]
		public async Task<ActionResult> Create([FromForm]int topicId, [FromForm] int? commentId, [FromForm] string content)
		{
			Comment comment = new Comment() { Content= content,TopicId=topicId };
			if(commentId != null)
			{
				comment.CommentId = (int)commentId;
			}
			bool b = await _commentService.CreateAsync(comment);
			if (b)
			{
				return Ok();
			} else
			{
				return BadRequest("创建失败");
			}
		}

		[HttpPost("update")]
		public async Task<ActionResult> Update([FromForm] int commentId, [FromForm] string content)
		{
			Comment comment = await _commentService.FindAsync(commentId);
			if (comment == null)
			{
				return BadRequest("没有这条评论");
			}
			comment.Content = content;
			bool b = await _commentService.EditAsync(comment);
			if (b)
			{
				return Ok();
			} else
			{
				return BadRequest("更新失败");
			}
		}
		[HttpPost("delete")]
		public async Task<ActionResult> Delete([FromForm] int commentId)
		{
			Comment comment = await _commentService.FindAsync(commentId);
			if (comment == null)
			{
				return BadRequest("没有这条评论");
			}
			bool b = await _commentService.DeleteAsync(commentId);
			if (b)
			{
				return Ok();
			} else
			{
				return BadRequest("删除失败");
			}
		}
	}
}
