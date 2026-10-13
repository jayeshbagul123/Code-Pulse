using Microsoft.EntityFrameworkCore;

namespace Test_1.Models
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option):base(option)
        {
            
        }

        public DbSet<Students> Students { get; set; }
    }
}
