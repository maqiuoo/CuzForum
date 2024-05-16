using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Interface
{
	public interface INotificationRepository : IBaseRepository<Notification>
	{
		Task<bool> Save(Notification notification);
		Task<long> CountNotRead(int targetUserId);
		Task<List<Notification>> FindByUserId(int targetUserId, int pageNo,int pageSize);
		Task<bool> Create(int topicId, int userId, int targetUserId, string action);
	}
}
