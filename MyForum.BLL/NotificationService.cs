using MyForum.BLL.Interface;
using MyForum.DAL;
using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.BLL
{
	public class NotificationService : BaseService<Notification>, INotificationService
	{
		private readonly INotificationRepository notificationRepository;

		public NotificationService(INotificationRepository notificationRepository)
		{
			base.baseRepository = notificationRepository;
			this.notificationRepository = notificationRepository;
		}
		public Task<long> CountNotRead(int targetUserId)
		{
			return notificationRepository.CountNotRead(targetUserId);
		}

		public Task<bool> Create(int topicId, int userId, int targetUserId, string action)
		{
			return notificationRepository.Create(topicId, userId, targetUserId, action);
		}

		public Task<List<Notification>> FindByUserId(int targetUserId, int pageNo, int pageSize)
		{
			return notificationRepository.FindByUserId(targetUserId, pageNo, pageSize);
		}

		public Task<bool> Save(Notification notification)
		{
			return notificationRepository.Save(notification);
		}

		public async Task UpdateRead(List<Notification> notifications)
		{
			// 开启一个新的任务以异步执行更新操作
			await Task.Run(async () =>
			{
				try
				{
					// 等待500毫秒
					await Task.Delay(500);

					foreach (var notification in notifications.Where(n => !n.Read))
					{
						// 假设这里的 Save 方法是异步的，并已经进行了适当的修改
						notification.Read = true;
						await SaveAsync(notification);
					}
				} catch (Exception e)
				{
					Console.WriteLine(e);
				}
			});
		}


		private Task SaveAsync(Notification notification)
		{
			notificationRepository.Save(notification);
			return Task.CompletedTask;
		}
	}
}
