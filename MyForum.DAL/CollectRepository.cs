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
	public class CollectRepository : BaseRepository<Collect>, ICollectRepository
	{
		public CollectRepository(ModelDbContext dbctx) : base(dbctx)
		{
		}

		public async Task<bool> DeleteByTopicId(int topicId)
		{
			var c = await dbctx.Collects.FirstOrDefaultAsync(c => c.TopicId == topicId);
			if (c != null)
			{
				dbctx.Collects.Remove(c);
				return await dbctx.SaveChangesAsync() > 0;
			} else
			{
				return false;
			}
		}

		public async Task<List<Collect>> FindByUserId(int userId, int pageNo, int pageSize)
		{
			var collects = dbctx.Collects.Skip((pageNo - 1) * pageSize).Take(pageSize).OrderByDescending(c => c.InTime);
			return await collects.Where(c => c.UserId == userId).ToListAsync();
		}

		public async Task<Collect> FindByUserIdAndTopicId(int userId, int topicId)
		{
			return await dbctx.Collects.FirstOrDefaultAsync(c => c.UserId == userId && c.TopicId == topicId);
		}
	}
}
