using Microsoft.EntityFrameworkCore;
using ZigitTest.Models;

namespace ZigitTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
    }
}
