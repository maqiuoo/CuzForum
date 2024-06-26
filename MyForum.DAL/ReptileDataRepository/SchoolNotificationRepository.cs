using MyForum.DAL.ReptileDataInterface;
using MyForum.Model.ReptlieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.ReptileDataRepository
{
	public class SchoolNotificationRepository : ReptileDataRepository<SchoolNotification>, ISchoolNotificationRepository
	{
		public SchoolNotificationRepository(ReptileDbContext dbctx) : base(dbctx)
		{
		}
	}
}
