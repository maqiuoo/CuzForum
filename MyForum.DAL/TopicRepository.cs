using Azure;
using Microsoft.EntityFrameworkCore;
using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL
{
	public class TopicRepository : BaseRepository<Topic>, ITopicRepository
	{
		public TopicRepository(ModelDbContext dbctx) : base(dbctx)
		{
		}

		public Task<List<Topic>> FindAll(int pageNo, int pageSize, string tab)
		{
			var topics = dbctx.Topics.Where(t => t.Tab == tab).Skip(pageSize).Take(pageSize);
			return (Task<List<Topic>>)topics;
		}

		public async Task<Topic> FindByTitle(string title)
		{
			return await dbctx.Topics.FirstOrDefaultAsync(t => t.Title == title);
		}

		public async Task<Topic> FindByUrl(string url)
		{
			return await dbctx.Topics.FirstOrDefaultAsync(t => t.Url == url);
		}

		public Task<List<Topic>> FindByUserId(int userId, int pageNo, int pageSize)
		{
			var topics = dbctx.Topics.Where(t => t.UserId == userId).Skip(pageSize).Take(pageSize);
			return (Task<List<Topic>>)topics;
		}
	}
}
