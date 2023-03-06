using Microsoft.EntityFrameworkCore;
using VTask.Model;

namespace VTask
{
    public class MainDatabaseContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UserTask> Tasks => Set<UserTask>();

        public MainDatabaseContext(DbContextOptions<MainDatabaseContext> options) : base(options)
        {
        }
    }
}
