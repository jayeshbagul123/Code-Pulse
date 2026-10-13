using Microsoft.EntityFrameworkCore;
using Web_API_Order.Models;

namespace Web_API_Order
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
                
        }

        public DbSet<Orders> Orders { get; set; }
        public DbSet<Students> Students { get; set; }
    }
}
