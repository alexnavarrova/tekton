
using System.Threading.Tasks;
using Tekton.Infraestructure.Services;

namespace Tekton.Infraestructure.Interfaces.DataAccess
{
	public interface IHttpService
	{
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T model);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T model);
        Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T model);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T model);
        Task<HttpResponseWrapper<object>> Delete(string url);
    }
}

