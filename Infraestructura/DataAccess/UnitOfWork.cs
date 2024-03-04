using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tekton.Core.Entities;
using Tekton.Infraestructure;
using Tekton.Infraestructure.DataAccess;
using Tekton.Infraestructure.Interfaces.DataAccess;

namespace Tekton.infraestructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private DbContext DbContext { get; set; }
        private IDbContextTransaction? _transaction;
        private IRepository<Product>? _productRepository;
        #endregion

        public UnitOfWork(TektonContext dbContext)
        {
            DbContext = dbContext;
        }

        #region Transactions
        public async Task BeginTransactionAsync()
        {
            _transaction ??= await DbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task CloseTransactionAsync()
        {
            if (_transaction != null) await _transaction.DisposeAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null) await _transaction.RollbackAsync();
        }
        #endregion

        #region Repositories
        public IRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository ??= new Repository<Product>(DbContext);
            }
        }

        #endregion
    }
}

