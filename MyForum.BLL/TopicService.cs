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
		private readonly ICommentService commentService;
		private readonly ICollectService collectService;

		public TopicService(ITopicRepository topicRepository, ICommentService commentService, ICollectService collectService)
		{
			base.baseRepository = topicRepository;
			this.topicRepository = topicRepository;
			this.commentService = commentService;
			this.collectService = collectService;
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
		public override async Task<bool> DeleteAsync(int id)
		{
			return await topicRepository.DeleteAsync(id) 
				&& await commentService.DeleteByTopicId(id)
				&& await collectService.DeleteByTopicId(id);	
		}
	}
}
