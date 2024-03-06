using Tekton.Domain.Entities;
using Tekton.Infraestructure.Services;
using Tekton.Application.Contracts.Persistence;

namespace Tekton.infraestructure.Repositories
{
    public class ProductStatusRepository : RepositoryBase<ProductStatus>, IProductStatusRepository
    {
        public ProductStatusRepository(TektonContext context) : base(context)
        {
        }
    }
}

