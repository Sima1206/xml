using Microsoft.EntityFrameworkCore;

namespace UserService.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }  
    }
}
