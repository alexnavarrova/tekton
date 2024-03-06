using MediatR;
using AutoMapper;
using Tekton.Domain.Entities;
using Tekton.Application.Messages;
using Tekton.Application.Services;
using Tekton.Application.Exceptions;
using Tekton.Application.Contracts.APIs;
using Tekton.Application.Contracts.Persistence;
using Tekton.Application.Features.Product.dtos;

namespace Tekton.Application.Features.Product.Queries
{
    public class ProductGetByIdQuery : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
    }

    public class ProductGetByIdQueryHandler : IRequestHandler<ProductGetByIdQuery, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiMockupService _apiMockupService;
        private readonly IProductStatusCacheInitializer _productStatusCacheInitializer;


        public ProductGetByIdQueryHandler(IUnitOfWork unitOfWork,
                IMapper mapper,
                IProductStatusCacheInitializer productStatusCacheInitializer,
                IApiMockupService apiMockupService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _apiMockupService = apiMockupService;
            _productStatusCacheInitializer = productStatusCacheInitializer;
        }

        public async Task<ProductDto> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId) ??
                    throw new NotFoundException(ErrorMessage.NotFoundEntityById.Replace("{entity}", nameof(Product) ).Replace("{id}", request.ProductId.ToString()), "");

            var productDto = _mapper.Map<Domain.Entities.Product, ProductDto>(product);

            var productStatuses = await _productStatusCacheInitializer.GetProductStatuses();

            var average = (float) await _apiMockupService.GetAverage(Guid.NewGuid());

            productDto.Average = average;

            productDto.FinalPrice = productDto.Price * ((100 - average) / 100);

            productDto.Status = productStatuses.FirstOrDefault(x => x.StatusId == product.StatusId)?.Description ??
                                    throw new NotFoundException(ErrorMessage.NotFoundEntityById.Replace("{entity}", nameof(ProductStatus)).Replace("{id}", product.StatusId.ToString()), ""); ;

            return productDto;
        }
    }
}

