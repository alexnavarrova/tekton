using Tekton.Domain.Entities;
using Tekton.Infraestructure.Common;
using Tekton.Application.Contracts.APIs;
using Tekton.Infraestructure.Interfaces.DataAccess;

namespace Tekton.Infraestructure.Services
{
	public class ApiMockupService: IApiMockupService
    {
		private readonly IHttpService _httpService;

		public ApiMockupService(IHttpService httpService)
		{
			_httpService = httpService;
        }

		public async Task<float> GetAverage(Guid productId)
		{
            string url = Path.Combine(ApiEndpoints.API_MOCK_URL, ApiEndpoints.API_MOCK_AVERAGE_PRODUCT, productId.ToString());

			var averageProduct = await _httpService.Get<AverageProduct>(url);

			if (averageProduct != null && averageProduct.Response != null)
				return averageProduct.Response.Average;

			return 0f;
        }
    }
}

