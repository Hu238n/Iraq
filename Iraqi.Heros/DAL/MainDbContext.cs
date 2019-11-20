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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=176.9.61.212;Port=5432;Database=Iraq;User Id=postgres;Password=aaa-1994");
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}