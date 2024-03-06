using MediatR;
using AutoMapper;
using Tekton.Application.Messages;
using Tekton.Application.Services;
using Tekton.Application.Exceptions;
using Tekton.Application.Features.Product.dtos;
using Tekton.Application.Contracts.Persistence;

namespace Tekton.Application.Features.Product.Queries
{
    public class ProductGetAllQuery : IRequest<IEnumerable<ProductDto>> { }

    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, IEnumerable<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductStatusCacheInitializer _productStatusCacheInitializer;

        public ProductGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IProductStatusCacheInitializer productStatusCacheInitializer)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productStatusCacheInitializer = productStatusCacheInitializer;
        }

        public async Task<IEnumerable<ProductDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync()
                            ?? throw new NotFoundException(ErrorMessage.NotFoundEntity.Replace("{entity}", "Products"), "");            

            var productStatuses = await _productStatusCacheInitializer.GetProductStatuses();

            return products.Select(p =>
            {
                var productDto = _mapper.Map<Domain.Entities.Product, ProductDto>(p);
                var productStatus = productStatuses.FirstOrDefault(x => x.StatusId == p.StatusId);
                productDto.Status = productStatus?.Description ?? "";
                return productDto;
            });
        }
    }
}

