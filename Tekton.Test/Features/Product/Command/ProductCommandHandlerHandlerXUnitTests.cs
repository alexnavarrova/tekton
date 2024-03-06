using Moq;
using MediatR;
using Shouldly;
using AutoMapper;
using Tekton.UnitTest.Mocks;
using Tekton.Application.Mappings;
using Tekton.Infraestructure.Services;
using Tekton.Application.Features.Product.Commands;

namespace Tekton.UnitTest.Features.Product.Queries
{
	public class ProductCommandHandlerXUnitTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public ProductCommandHandlerXUnitTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            MockProductRepository.AddDataProductRepository(_unitOfWork.Object.TektonContext);
        }


        [Fact]
        public async Task CreateProductCommand_InputProduct_ReturnsUnit()
        {
            var productCreate = new ProductCreateCommand
            {
                Name = "Product1",
                Description = "Descrition of Product",
                StatusId = 0,
                Price = 1200,
                SKU = "AAAAK" 
            };

            var handler = new ProductCreateCommandHandler(_unitOfWork.Object, _mapper);

            var result = await handler.Handle(productCreate, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}

