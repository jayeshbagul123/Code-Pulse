using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace WebApp_Bulk.Models
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
