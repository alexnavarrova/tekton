using Tekton.Domain.Entities;
using Tekton.Infraestructure.Services;
using Tekton.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Tekton.infraestructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(TektonContext context) : base(context)
        {
        }

        public async Task<Product> GetProductBySku(string sku)
        {
            return await _context.Products.Where(v => v.SKU == sku)?.FirstOrDefaultAsync();
        }
    }
}

