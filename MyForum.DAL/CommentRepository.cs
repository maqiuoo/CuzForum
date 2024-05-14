using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL
{
	public class CommentRepository : BaseRepository<Comment>, ICommentRepository
	{
		public CommentRepository(ModelDbContext dbctx) : base(dbctx)
		{
		}

		public async Task<bool> DeleteByTopicId(int topicId)
		{
			Comment comment = await dbctx.Comments.FindAsync(topicId);
			if(comment != null)
			{
				dbctx.Comments.Remove(comment);
				return await dbctx.SaveChangesAsync() > 0;
			} else
			{
				return false;
			}
		}

		public async Task<List<Comment>> FindByTopicId(int topicId)
		{
			var comments = dbctx.Comments.Where(c => c.TopicId == topicId).ToList();
			return comments;
		}

		public async Task<List<Comment>> FindByUserId(int userId)
		{
			var comments = dbctx.Comments.Where(c => c.UserId == userId).ToList();
			return comments;
		}
	}
}
