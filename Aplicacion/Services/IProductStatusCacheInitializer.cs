using Tekton.Domain.Entities;

namespace Tekton.Application.Services
{
	public interface IProductStatusCacheInitializer
	{
        Task<List<ProductStatus>> GetProductStatuses();
    }
}

