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
	public class CollectService : BaseService<Collect>, ICollectService
	{
		private readonly ICollectRepository collectRepository;

		public CollectService(ICollectRepository collectRepository)
		{
			base.baseRepository = collectRepository;
			this.collectRepository = collectRepository;
		}
		public Task<bool> DeleteByTopicId(int topicId)
		{
			return collectRepository.DeleteByTopicId(topicId);
		}

		public Task<List<Collect>> FindByUserId(int userId, int pageNo, int pageSize)
		{
			return collectRepository.FindByUserId(userId, pageNo, pageSize);
		}

		public Task<Collect> FindByUserIdAndTopicId(int userId, int topicId)
		{
			return collectRepository.FindByUserIdAndTopicId(userId, topicId);
		}
	}
}
