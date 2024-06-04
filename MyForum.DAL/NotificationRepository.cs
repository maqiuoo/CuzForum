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
	public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
	{
		public NotificationRepository(ModelDbContext dbctx) : base(dbctx)
		{
		}

		public async Task<long> CountNotRead(int targetUserId)
		{
			long count = await dbctx.Notifications.CountAsync(n => n.Read == false && n.TargetUserId == targetUserId);
			return count;
		}

		public async Task<bool> Create(int topicId, int userId, int targetUserId, string action)
		{
			if(userId != targetUserId)
			{
				Notification notification = new Notification() {
					TopicId = topicId,
					UserId = userId,
					Action = action,
					TargetUserId = targetUserId,
					InTime = DateTime.Now,
					Read = false
				};
				dbctx.Notifications.Add(notification);
				return await dbctx.SaveChangesAsync() > 0;
			} else
			{ return false; }
		}

		public async Task<List<Notification>> FindByUserId(int targetUserId, int pageNo, int pageSize)
		{
			var notifications = dbctx.Notifications
				.Skip((pageNo - 1) * pageSize).Take(pageSize).OrderBy(x=>x.Read).ThenBy(x=>x.InTime);
			return notifications.Where(x => x.TargetUserId == targetUserId).ToList();
		}

		public async Task<bool> Save(Notification notification)
		{
			await dbctx.Notifications.AddAsync(notification);
			return await dbctx.SaveChangesAsync() > 0;
		}
	}
}
