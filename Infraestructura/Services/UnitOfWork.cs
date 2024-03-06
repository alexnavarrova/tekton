using Tekton.Domain;
using System.Collections;
using Tekton.Infraestructure.Interfaces.DataAccess;
using Tekton.infraestructure.Repositories;
using Tekton.Application.Contracts.Persistence;

namespace Tekton.Infraestructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private readonly TektonContext _context;

        private Hashtable _repositories;

        public TektonContext TektonContext => _context;

        private IProductRepository _productRepository;
        private IProductStatusRepository _productStatusRepository;

        #endregion

        #region repositoris

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
        public IProductStatusRepository ProductStatusRepository => _productStatusRepository ??= new ProductStatusRepository(_context);

        #endregion

        public UnitOfWork(TektonContext context)
        {
            _context = context;
        }

        

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}

