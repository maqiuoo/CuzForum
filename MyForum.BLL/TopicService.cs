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
	public class TopicService : BaseService<Topic>, ITopicService
	{
		private readonly ITopicRepository topicRepository;

		public TopicService(ITopicRepository topicRepository)
		{
			base.baseRepository = topicRepository;
			this.topicRepository = topicRepository;
		}
		public Task<List<Topic>> FindAll(int pageNo, int pageSize, string tab)
		{
			return topicRepository.FindAll(pageNo, pageSize, tab);
		}

		public Task<Topic> FindByTitle(string title)
		{
			return topicRepository.FindByTitle(title);
		}

		public Task<Topic> FindByUrl(string url)
		{
			return topicRepository.FindByUrl(url);
		}

		public Task<List<Topic>> FindByUserId(int userId, int pageNo, int pageSize)
		{
			return topicRepository.FindByUserId(userId, pageNo, pageSize);
		}
	}
}
