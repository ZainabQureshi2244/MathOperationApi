using Microsoft.EntityFrameworkCore;
using ERP_dl.Entities;

namespace  ERP_dl.Data;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Operation> Operations { get; set; }
    }


