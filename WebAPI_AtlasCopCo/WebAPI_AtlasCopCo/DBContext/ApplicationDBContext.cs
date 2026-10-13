using Microsoft.EntityFrameworkCore;
using WebAPI_AtlasCopCo.Model;

namespace WebAPI_AtlasCopCo.DBContext
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Students> Students { get; set; }
    }
}
