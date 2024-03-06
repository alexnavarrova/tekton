using Tekton.Domain.Entities;
using Tekton.Application.Messages;
using Tekton.Application.Exceptions;
using Tekton.Application.Contracts.Persistence;

namespace Tekton.Application.Services
{
    public class ProductStatusCacheInitializer : IProductStatusCacheInitializer
    {
        private readonly ICacheRepository<List<ProductStatus>> _cacheRepository;
        private readonly IUnitOfWork _unitOfWork;

        private const string ProductStatusKey = "ProductStatuses";
        private const int ProductStatusCacheMinute = 5;


        public ProductStatusCacheInitializer(ICacheRepository<List<ProductStatus>> cacheRepository, IUnitOfWork unitOfWork)
        {
            _cacheRepository = cacheRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<List<ProductStatus>> GetProductStatuses()
        {

            var alreadyExist = _cacheRepository.Get(ProductStatusKey);

            if (alreadyExist == null || alreadyExist.Count() == 0)
            {
                var productStatusList = await _unitOfWork.ProductStatusRepository.GetAllAsync() ??
                            throw new NotFoundException(ErrorMessage.NotFoundEntity.Replace("{entity}", nameof(ProductStatus)), "");

                _cacheRepository.Set(ProductStatusKey, productStatusList.ToList(), TimeSpan.FromMinutes(ProductStatusCacheMinute));
            }

            return _cacheRepository.Get(ProductStatusKey);
        }
    }
}

