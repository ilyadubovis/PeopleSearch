using Microsoft.EntityFrameworkCore;

namespace PeopleSearch.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {       
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.Seed(); // extension method (see ModelBuilderExtensions class)
    }
}
