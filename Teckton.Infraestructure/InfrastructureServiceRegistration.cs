using Microsoft.EntityFrameworkCore;
using Tekton.Infraestructure.Services;
using Tekton.Application.Contracts.APIs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tekton.Application.Contracts.Persistence;
using Tekton.Infraestructure.Interfaces.DataAccess;

namespace Tekton.Infraestructure
{
	public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");

            services.AddDbContext<TektonContext>(options => options.UseNpgsql(connectionString));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IHttpService, HttpService>();

            services.AddTransient<IApiMockupService, ApiMockupService>();   

            return services;
        }
    }
}

