using Dot_net_ModelViewController.Models;
using Microsoft.EntityFrameworkCore;

namespace Dot_net_ModelViewController.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
