using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppdbContext : DbContext
    {
        public AppdbContext(DbContextOptions<AppdbContext> db) : base(db)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
