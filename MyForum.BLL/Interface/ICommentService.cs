using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BLL.Interface
{
	public interface ICommentService : IBaseService<Comment>
	{
		Task<List<Comment>> FindByTopicId(int topicId);
		Task<bool> DeleteByTopicId(int topicId);
		Task<List<Comment>> FindByUserId(int userId);
	}
}
