using MyForum.Model;
using MyForum.Model.ReptlieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.ReptileDataInterface
{
    public interface IReptileDataRepository<T> where T : class, IReptlieEntity
    {
        Task AddEntityAsync(T entity);
        Task AddRangeEntityAsync(List<T> entities);
        void UpdateEntity(T entity);
        T FindByTitle(string title);
        Task<List<T>> GetAll();
        Task<int> SaveChangesAsync();
    }
}
