using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.VisualBasic;
using MyForum.DAL.ReptileDataInterface;
using MyForum.Model;
using MyForum.Model.ReptlieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.ReptileDataRepository
{
    public class ReptileDataRepository<T> : IReptileDataRepository<T> where T : class, IReptlieEntity
    {
        private readonly ReptileDbContext dbctx;
        public ReptileDataRepository(ReptileDbContext dbctx)
        {
            this.dbctx = dbctx;
        }

        public async Task AddEntityAsync(T entity)
        {
            await dbctx.Set<T>().AddAsync(entity);
        }
        public void UpdateEntity(T entity)
        {
            T oldEntity = dbctx.Set<T>().FirstOrDefault(e => e.title == entity.title);
            var entityProperties = typeof(T).GetProperties();
            foreach (var property in entityProperties)
            {
                if (property.CanWrite && property.Name!= "Id")
                {
                    var newValue = property.GetValue(entity);
                    property.SetValue(oldEntity, newValue);
                }
            }
        }
        public T FindByTitle(string title)
        {
            return dbctx.Set<T>().FirstOrDefault(t => t.title == title);
        }
        public async Task<List<T>> GetAll()
        {
            return dbctx.Set<T>().ToList();
        }

        public async Task AddRangeEntityAsync(List<T> entities)
        {
            await dbctx.Set<T>().AddRangeAsync(entities);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await dbctx.SaveChangesAsync();
        }
    }
}
