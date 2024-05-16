using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Interface
{
	public interface ICollectRepository : IBaseRepository<Collect>
	{
		Task<bool> DeleteByTopicId(int topicId);
		Task<List<Collect>> FindByUserId(int userId, int pageNo, int pageSize);
		Task<Collect> FindByUserIdAndTopicId(int userId,int topicId);
	}
}
