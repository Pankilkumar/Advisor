using Microsoft.EntityFrameworkCore;

namespace advisor_backend.Models // Correct the namespace to match the Startup.cs file
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Advisor> Advisors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed the initial data
            modelBuilder.Entity<Advisor>().HasData(new Advisor
            {
                Name = "Alice Johnson",
                SIN = "456789012",
                Address = "789 Oak St",
                Phone = "45678901"
                
            });
        }
    }
}