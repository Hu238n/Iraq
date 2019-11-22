using Iraqi.Heros.Models;
using Microsoft.EntityFrameworkCore;

namespace Iraqi.Heros.DAL
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
        }

        public MainDbContext()
        {
        }
 

        public DbSet<Person> Persons { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}