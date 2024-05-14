using Azure;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Interface
{
	public interface ITopicRepository : IBaseRepository<Topic>
	{
		public Task<List<Topic>> FindByUserId(int userId, int pageNo, int pageSize);
		public Task<Topic> FindByUrl(string url);
		public Task<Topic> FindByTitle(string title);
		public Task<List<Topic>> FindAll(int pageNo, int pageSize,string tab);
	}
}
