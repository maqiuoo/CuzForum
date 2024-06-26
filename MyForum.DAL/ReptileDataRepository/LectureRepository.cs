using MyForum.DAL.ReptileDataInterface;
using MyForum.Model.ReptlieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.ReptileDataRepository
{
	public class LectureRepository : ReptileDataRepository<Lecture>,ILectureRepository
	{
		public LectureRepository(ReptileDbContext dbctx) : base(dbctx)
		{
		}
	}
}
