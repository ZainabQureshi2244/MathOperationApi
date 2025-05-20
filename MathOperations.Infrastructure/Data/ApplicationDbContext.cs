using Microsoft.EntityFrameworkCore;
using MathOperation.Domain.Entities;

namespace MathOperation.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Operation> Operations { get; set; }
    }
}
