using MediatR;
using AutoMapper;
using Tekton.Application.Features.Product.dtos;
using Tekton.Application.Contracts.Persistence;

namespace Tekton.Application.Features.Product.Commands
{
    public class ProductCreateCommand : ProductCreateDto, IRequest { }

    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductCreateDto, Domain.Entities.Product>(request);

            var skuProduct = await _unitOfWork.ProductRepository.GetProductBySku(request.SKU);

            if(skuProduct != null)
                throw new Exception($"No se pudo insertar el registro, sku existe");

            _unitOfWork.ProductRepository.AddEntity(product);

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception($"No se pudo insertar el record de streamer");
            }

            return Unit.Value;
            
        }
    }
}

