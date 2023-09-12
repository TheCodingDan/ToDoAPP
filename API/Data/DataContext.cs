using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Domain.Entities.Task> Tasks{ get; set; }
        public DbSet<User> Users { get; set; }

    }
}