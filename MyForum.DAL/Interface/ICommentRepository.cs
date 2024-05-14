using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Interface
{
	public interface ICommentRepository : IBaseRepository<Comment>
	{
		Task<List<Comment>> FindByTopicId(int topicId);
		Task<bool> DeleteByTopicId(int topicId);
		Task<List<Comment>> FindByUserId(int userId);
	}
}
