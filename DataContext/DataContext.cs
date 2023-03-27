using tarefasbackend.Models;
using Microsoft.EntityFrameworkCore;

namespace tarefasbackend.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) {}

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

    }

}