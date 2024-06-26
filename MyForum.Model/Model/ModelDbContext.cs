using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model.Model
{
    public class ModelDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Collect> Collects { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<JwtUser> JwtUsers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EmploymentData> EmploymentDatas { get; set; }
        public ModelDbContext() { }
        public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
               .HasOne(t => t.User)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);
            // 指明一个Topic有一个User

            modelBuilder.Entity<Notification>()
               .HasOne(t => t.User)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Notification>()
               .HasOne(t => t.Topic)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
               .HasOne(t => t.User)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
               .HasOne(t => t.Topic)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
               .HasOne(t => t.SecondComment)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Collect>()
               .HasOne(t => t.User)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Collect>()
               .HasOne(t => t.Topic)
               .WithMany()
               .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
