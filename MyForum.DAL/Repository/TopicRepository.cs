using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
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
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(ModelDbContext dbctx) : base(dbctx)
        {
        }

        public async Task<List<Topic>> FindAll(int pageNo, int pageSize, string? tab)
        {
            var t = dbctx.Topics.OrderByDescending(t => t.Top).ThenByDescending(t => t.InTime);
            var topics = t.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            if (string.IsNullOrEmpty(tab))
            {
                return topics;
            }
            else
            {
                return t.Where(t => t.Tab == tab).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }

        }
        public async Task<int> GetAll()
        {
            var t = dbctx.Topics.ToList();
            return t.Count;
        }
        public async Task<Topic> FindByTitle(string title)
        {
            return await dbctx.Topics.FirstOrDefaultAsync(t => t.Title == title);
        }

        public async Task<Topic> FindByUrl(string url)
        {
            return await dbctx.Topics.FirstOrDefaultAsync(t => t.Url == url);
        }

        public async Task<List<Topic>> FindByUserId(int userId, int pageNo, int pageSize)
        {
            var topics = dbctx.Topics.Where(t => t.UserId == userId).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            return topics;
        }
    }
}
