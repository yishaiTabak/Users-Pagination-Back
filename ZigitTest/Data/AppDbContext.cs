using Microsoft.EntityFrameworkCore;
using ZigitTest.Models;

namespace ZigitTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<EmailProvider> EmailProviders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.Name)
            .HasDatabaseName("IX_Name");


            modelBuilder.Entity<EmailProvider>().HasData(
                new EmailProvider { Name = "gmail" },
                new EmailProvider { Name = "yahoo" },
                new EmailProvider { Name = "hotmail" }
            );
        }
    }
}
