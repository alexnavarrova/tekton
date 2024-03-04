using MediatR;
using AutoMapper;
using Tekton.Core.Messages;
using Tekton.Application.Features.Product.dtos;
using Tekton.Infraestructure.Interfaces.DataAccess;
using Tekton.Application.Exceptions;

namespace Tekton.Application.Features.Product.Queries
{
    public class ProductGetByIdQuery : IRequest<ProductDto>
    {
        public Guid ProductId { get; set; }
    }

    public class ProductGetByIdQueryHandler : IRequestHandler<ProductGetByIdQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(
                where: x =>
                x.ProductId == request.ProductId) ??
                    throw new NotFoundException(ErrorMessage.NotFoundEntityById.Replace("{entity}", "Product").Replace("{id}", request.ProductId.ToString()), "");

            return _mapper.Map<Core.Entities.Product, ProductDto>(data);
        }
    }
}

