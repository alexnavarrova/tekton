using Microsoft.EntityFrameworkCore;
using Tekton.Core.Entities;

namespace Tekton.Infraestructure
{
    public partial class TektonContext : DbContext
    {
        public TektonContext(DbContextOptions<TektonContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products => Set<Product>();

    }
}

