namespace Tekton.Application.Contracts.APIs
{
	public interface IApiMockupService
	{
        Task<float> GetAverage(Guid productId);
    }
}

