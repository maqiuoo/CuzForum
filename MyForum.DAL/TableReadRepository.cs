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

		public async Task<List<EmploymentData>> FindByYear(int year, string type)
		{
			if(type.Equals("chinamap"))
			{
				return dbctx.EmploymentDatas.Where(e => e.Year == year && e.Type == "全国").ToList();
			}
			if(type.Equals("mobility"))
			{
				return dbctx.EmploymentDatas.Where(e => e.Year == year && e.Type == "省内").ToList();
			}
			return null;
		}
	}
}
