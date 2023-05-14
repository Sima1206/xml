using Microsoft.EntityFrameworkCore;

namespace XML.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public ApplicationContext() { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured)
            {
                return;
            }
            builder.UseSqlServer("Server=mssql;Database=XML;User Id=sa;Password=Your_password123!;");

        }


    }
}
