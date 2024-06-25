using MyForum.BLL.Interface;
using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BLL
{
	public class CommentService : BaseService<Comment>, ICommentService
	{
		private readonly ICommentRepository commentRepository;

		public CommentService(ICommentRepository commentRepository)
		{
			base.baseRepository = commentRepository;
			this.commentRepository = commentRepository;
		}
		public Task<bool> DeleteByTopicId(int topicId)
		{
			return commentRepository.DeleteByTopicId(topicId);
		}

		public Task<List<Comment>> FindByTopicId(int topicId)
		{
			return commentRepository.FindByTopicId(topicId);
		}

		public Task<List<Comment>> FindByUserId(int userId)
		{
			return commentRepository.FindByUserId(userId);
		}
		public async Task<List<Comment>> FindByUserId(int userId, int pageNo, int pageSize)
		{
			return await commentRepository.FindByUserId(userId, pageNo, pageSize);
		}
	}
}
