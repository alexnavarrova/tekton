using Tekton.Domain.Entities;

namespace Tekton.Application.Contracts.Persistence
{
	public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<Product>? GetProductBySku(string sku);
    }
}

