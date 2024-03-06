
using Tekton.Domain;

namespace Tekton.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {

        IProductRepository ProductRepository { get; }

        IProductStatusRepository ProductStatusRepository { get; }

        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

        Task<int> Complete();
    }
}

