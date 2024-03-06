using AutoMapper;
using Tekton.Domain.Entities;
using Tekton.Application.Features.Product.dtos;

namespace Tekton.Application.Mappings
{
	public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
        }
	}
}

