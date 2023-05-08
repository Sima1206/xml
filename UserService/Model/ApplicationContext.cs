using Microsoft.EntityFrameworkCore;

namespace UserService.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        { }

        public ApplicationContext() { }


        public DbSet<User> Users { get; set; }

    //DESKTOP-HE4F5VO
        protected override void OnConfiguring(DbContextOptionsBuilder builder) 
        {
            if (builder.IsConfigured)
            {
                return;
            }

            builder.UseSqlServer("Data Source=DESKTOP-HE4F5VO;Initial Catalog=User;Integrated Security=true;");
        }
    }
}
