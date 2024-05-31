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
		 Task<List<Topic>> FindByUserId(int userId, int pageNo, int pageSize);
		 Task<Topic> FindByUrl(string url);
		 Task<Topic> FindByTitle(string title);
		 Task<List<Topic>> FindAll(int pageNo, int pageSize,string tab);
		 Task<int> GetAll();
	}
}
