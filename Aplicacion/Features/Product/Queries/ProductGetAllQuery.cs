using MediatR;
using AutoMapper;
using Tekton.Core.Messages;
using Tekton.Application.Features.Product.dtos;
using Tekton.Infraestructure.Interfaces.DataAccess;
using Tekton.Application.Exceptions;

namespace Tekton.Application.Features.Product.Queries
{
    public class ProductGetAllQuery : IRequest<IEnumerable<ProductDto>> { }

    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, IEnumerable<ProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.ProductRepository.GetAllAsync() ?? throw new NotFoundException(ErrorMessage.NotFoundEntity.Replace("{entity}", "Products"), "");            
            return _mapper.Map<IEnumerable<Core.Entities.Product>, IEnumerable<ProductDto>>(data);
        }
    }
}

