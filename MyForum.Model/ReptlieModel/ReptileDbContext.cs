using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyForum.Model.ReptlieModel
{
    public class ReptileDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<SchoolNotification> SchoolNotifications { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public ReptileDbContext() { }
        public ReptileDbContext(DbContextOptions<ReptileDbContext> options) : base(options) { }
    }
}
