using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model
{
	public class ModelDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Topic> Topics { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Collect> Collects {  get; set; }
		public DbSet<AccessToken> AccessTokens { get; set; }
		public DbSet<JwtUser> JwtUsers { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public ModelDbContext() { }
		public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
		}
	}
}
