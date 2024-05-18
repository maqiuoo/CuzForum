using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Interface
{
    public interface ITableReadRepository : IBaseRepository<EmploymentData>
    {
        Task<bool> AddRange(List<EmploymentData> employments);
    }
}
