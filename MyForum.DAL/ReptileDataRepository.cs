using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using MyForum.DAL.Interface;
using MyForum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.DAL
{
	public class ReptileDataRepository : IReptileDataRepository
	{
		private readonly ReptileDbContext dbctx;
		public ReptileDataRepository(ReptileDbContext dbctx)
		{
			this.dbctx = dbctx;
		}

		public async Task<int> AddOrUpdate(List<ReptileData>  books)
		{
			foreach(var book in books)
			{
				ReptileData reptileData = await dbctx.ReptileDatas.FirstOrDefaultAsync(b => b.BookTitle == book.BookTitle);
				if (reptileData == null)
				{
					await dbctx.ReptileDatas.AddAsync(new ReptileData() { BookTitle = book.BookTitle, Clicks = book.Clicks });
				} else
				{
					reptileData.Clicks = book.Clicks;
				}
			}
			return await dbctx.SaveChangesAsync();
		}
	}
}
