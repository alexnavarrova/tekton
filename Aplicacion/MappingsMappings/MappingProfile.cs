using AutoMapper;
using Tekton.Application.Features.Product.dtos;
using Tekton.Core.Entities;

namespace Tekton.Application.Mappings
{
	public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            /*
            CreateMap<ImmediateReturnDetailDto, ImmediateReturnDetail>()
                 .ForMember(p => p.Referencia, x => x.MapFrom(a => a.Reference))
                 .ForMember(p => p.Cantidad, x => x.MapFrom(a => a.Quantity))
                 .ForMember(p => p.ValorSaldo, x => x.MapFrom(a => a.BalanceValue));
            */

            CreateMap<Product, ProductDto>();
        }
	}
}

