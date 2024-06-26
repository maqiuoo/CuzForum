using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using MyForum.DAL.Interface;
using MyForum.Model;
using MyForum.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Repository
{
    public class TableReadRepository : BaseRepository<EmploymentData>, ITableReadRepository
    {
        public TableReadRepository(ModelDbContext dbctx) : base(dbctx)
        {
        }

        public async Task<bool> UpdateAndCreate(List<EmploymentData> employments)
        {
            foreach (var item in employments)
            {
                var e = await dbctx.EmploymentDatas.FirstOrDefaultAsync(d => d.Region == item.Region && item.Year == d.Year);
                if (e == null)
                {
                    await dbctx.EmploymentDatas.AddAsync(item);
                }
                else
                {
                    e.Region = item.Region;
                    e.Population = item.Population;
                    e.Year = item.Year;
                }
            }
            return await dbctx.SaveChangesAsync() > 0;
        }

        public async Task<List<EmploymentData>> FindByYear(int year, string type)
        {
            if (type.Equals("chinamap"))
            {
                return dbctx.EmploymentDatas.Where(e => e.Year == year && e.Type == "全国").ToList();
            }
            if (type.Equals("mobility"))
            {
                return dbctx.EmploymentDatas.Where(e => e.Year == year && e.Type == "省内").ToList();
            }
            return null;
        }
    }
}
