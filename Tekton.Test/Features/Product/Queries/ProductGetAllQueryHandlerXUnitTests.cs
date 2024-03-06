using Moq;
using Shouldly;
using AutoMapper;
using Tekton.UnitTest.Mocks;
using Tekton.Domain.Entities;
using Tekton.Application.Services;
using Tekton.Application.Mappings;
using Tekton.Infraestructure.Services;
using Tekton.Application.Contracts.APIs;
using Tekton.Application.Features.Product.Queries;
using Tekton.Application.Features.Product.dtos;

namespace Tekton.UnitTest.Features.Product.Queries
{
	public class ProductGetAllQueryHandlerXUnitTests
	{
        private readonly IMapper _mapper;
        private readonly Mock<UnitOfWork> _unitOfWork;

        public ProductGetAllQueryHandlerXUnitTests()
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
        public async Task GetProductlAllTest()
        {
            var mockProductStatusCacheInitializer = new Mock<IProductStatusCacheInitializer>();

            mockProductStatusCacheInitializer.Setup(x => x.GetProductStatuses())
            .ReturnsAsync(new List<ProductStatus>
            {
                new ProductStatus { StatusId = 0, Description = "Active" },
                new ProductStatus { StatusId = 1, Description = "Inactive" }
            });

            var handler = new ProductGetAllQueryHandler(_unitOfWork.Object, _mapper, mockProductStatusCacheInitializer.Object);

            var request = new ProductGetAllQuery();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result);

        }

        [Fact]
        public async Task GetProductlByIdTest()
        {
            var mockProductStatusCacheInitializer = new Mock<IProductStatusCacheInitializer>();

            mockProductStatusCacheInitializer.Setup(x => x.GetProductStatuses())
                .ReturnsAsync(new List<ProductStatus>
                {
                    new ProductStatus { StatusId = 0, Description = "Active" },
                    new ProductStatus { StatusId = 1, Description = "Inactive" }
                });

            var mockApiMockupService = new Mock<IApiMockupService>();

            mockApiMockupService.Setup(x => x.GetAverage(Guid.NewGuid()))
               .ReturnsAsync(10);

            var handler = new ProductGetByIdQueryHandler(_unitOfWork.Object, _mapper, mockProductStatusCacheInitializer.Object, mockApiMockupService.Object);

            var request = new ProductGetByIdQuery
            {
                ProductId = 1
            };

            var result = await handler.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<ProductDto>();
        }

        [Fact]
        public async Task GetProductlByIdCheckValuesTest()
        {
            var mockProductStatusCacheInitializer = new Mock<IProductStatusCacheInitializer>();

            mockProductStatusCacheInitializer.Setup(x => x.GetProductStatuses())
                .ReturnsAsync(new List<ProductStatus>
                {
                    new ProductStatus { StatusId = 0, Description = "Active" },
                    new ProductStatus { StatusId = 1, Description = "Inactive" }
                });

            var mockApiMockupService = new Mock<IApiMockupService>();

            float average = 10; 
            
            mockApiMockupService.Setup(x => x.GetAverage(It.IsAny<Guid>()))
               .ReturnsAsync(average);

            var handler = new ProductGetByIdQueryHandler(_unitOfWork.Object, _mapper, mockProductStatusCacheInitializer.Object, mockApiMockupService.Object);

            var request = new ProductGetByIdQuery
            {
                ProductId = 1
            };

            var result = await handler.Handle(request, CancellationToken.None);

            var finalPrice = result.Price * ((100 - average) / 100);

            result.FinalPrice.ShouldBe(finalPrice);
            result.Average.ShouldBe(average);
            result.Status.ShouldBe("Active");
            result.Name.ShouldBe("Producto1"); 
            result.Description.ShouldBe("Producto 1");
            result.SKU.ShouldBe("F83FD");
            result.Stock.ShouldBe(15);
            result.Price.ShouldBe(1000);

        }


    }
}

