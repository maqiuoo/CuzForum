using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL
{
	public class TableReadRepository : BaseRepository<EmploymentData>, ITableReadRepository
	{
		public TableReadRepository(ModelDbContext dbctx) : base(dbctx)
		{
		}

		public async Task<bool> AddRange(List<EmploymentData> employments)
		{
			await dbctx.EmploymentDatas.AddRangeAsync(employments);
			return await dbctx.SaveChangesAsync() > 0;
		}
	}
}
