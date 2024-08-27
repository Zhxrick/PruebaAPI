using Microsoft.EntityFrameworkCore;
using Prueba_Back.Models;

namespace Prueba_Back
{
    public class AplicationDBcontext : DbContext
    {
        public AplicationDBcontext(DbContextOptions<AplicationDBcontext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
