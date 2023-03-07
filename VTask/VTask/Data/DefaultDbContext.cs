using Microsoft.EntityFrameworkCore;
using VTask.Model;

namespace VTask.Data
{
    public class DefaultDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UserTask> Tasks => Set<UserTask>();

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add some initial models

            base.OnModelCreating(modelBuilder);
        }
    }
}
