using AutoFixture;
using Tekton.Domain.Entities;
using Tekton.Infraestructure.Services;

namespace Tekton.UnitTest.Mocks
{
	public class MockProductRepository
	{
        public static void AddDataProductsRepository(TektonContext tektonContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var products = fixture.CreateMany<Product>().ToList();

            products.Add(fixture.Build<Product>()
                .With(tr => tr.ProductId, 1)
                .With(tr => tr.Name, "Producto1")
                .With(tr => tr.Description, "Producto 1")
                .With(tr => tr.SKU, "F83FD")
                .With(tr => tr.Stock, 15)
                .With(tr => tr.StatusId, 0)
                .With(tr => tr.Price, 1000)
                .Create()
            );

            products.Add(fixture.Build<Product>()
                .With(tr => tr.ProductId, 2)
                .With(tr => tr.Name, "Producto2")
                .With(tr => tr.Description, "Producto 2")
                .With(tr => tr.SKU, "F83F4")
                .With(tr => tr.Stock, 10)
                .With(tr => tr.StatusId, 1)
                .With(tr => tr.Price, 2000)
                .Create()
            );

            tektonContextFake.Products!.AddRange(products);
            tektonContextFake.SaveChanges();
        }

        public static void AddDataProductRepository(TektonContext tektonContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var products = fixture.CreateMany<Product>().ToList();

            products.Add(fixture.Build<Product>()
                .With(tr => tr.ProductId, 1)
                .With(tr => tr.Name, "Producto1")
                .With(tr => tr.Description, "Producto 1")
                .With(tr => tr.SKU, "F83FD")
                .With(tr => tr.Stock, 15)
                .With(tr => tr.StatusId, 0)
                .With(tr => tr.Price, 1000)
                .Create()
            );

            tektonContextFake.Products!.AddRange(products);
            tektonContextFake.SaveChanges();
        }
    }
}

