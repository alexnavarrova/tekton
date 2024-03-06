using Microsoft.EntityFrameworkCore;
using Tekton.Domain.Entities;

namespace Tekton.Infraestructure.Services
{
    public partial class TektonContext : DbContext
    {
        public TektonContext(DbContextOptions<TektonContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductStatus> ProductStatuses => Set<ProductStatus>();
        

    }
}

