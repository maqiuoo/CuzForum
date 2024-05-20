using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL.Interface
{
	public interface IReptileDataRepository
	{
		Task<int> AddOrUpdate(List<ReptileData> books);
		Task<List<ReptileData>> GetAll(); 
	}
}
