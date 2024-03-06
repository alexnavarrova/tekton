using AutoFixture;
using Tekton.Domain.Entities;
using Tekton.Infraestructure.Services;

namespace Tekton.UnitTest.Mocks
{
	public class MockProductStatusRepository
    {
        public static void AddDataProductRepository(TektonContext tektonContextFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var productStatuses = fixture.CreateMany<ProductStatus>().ToList();

            productStatuses.Add(fixture.Build<ProductStatus>()
                .With(tr => tr.StatusId, 0)
                .With(tr => tr.Description, "Active")
                .Create()
            );

            productStatuses.Add(fixture.Build<ProductStatus>()
                .With(tr => tr.StatusId, 1)
                .With(tr => tr.Description, "Inactive")
                .Create()
            );

            tektonContextFake.ProductStatuses!.AddRange(productStatuses);
            tektonContextFake.SaveChanges();
        }
    }
}

