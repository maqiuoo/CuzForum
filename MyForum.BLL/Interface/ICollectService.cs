using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BLL.Interface
{
	public interface ICollectService : IBaseService<Collect>
	{
		Task<bool> DeleteByTopicId(int topicId);
		Task<List<Collect>> FindByUserId(int userId, int pageNo, int pageSize);
		Task<Collect> FindByUserIdAndTopicId(int userId, int topicId);
	}
}
