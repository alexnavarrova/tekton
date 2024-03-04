using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tekton.infraestructure.DataAccess;
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

            //services.AddScoped<ISyncRepository, SyncRepository>();

            return services;
        }
    }
}

