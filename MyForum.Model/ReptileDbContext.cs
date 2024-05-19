using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model
{
	public class ReptileDbContext : DbContext
	{
		public DbSet<ReptileData> ReptileDatas {  get; set; }
		public ReptileDbContext() { }
		public ReptileDbContext(DbContextOptions<ReptileDbContext> options) : base(options) { }
	}
}
