using MediatR;
using FluentValidation;
using System.Reflection;
using Tekton.Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Tekton.Application.Services;
using Tekton.Application.Models;

namespace Tekton.Application
{
	public static class ApplicationServiceRegistrations
	{
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<Parameters>(c => configuration.GetSection("Parameters"));

            services.Configure<Parameters>(options =>
            {
                var parametersSection = configuration.GetSection("Parameters");
                options.PathPerformanceFile = parametersSection["PathPerformanceFile"];
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddScoped(typeof(ICacheRepository<>), typeof(MemoryCacheRepository<>));

            services.AddScoped<IProductStatusCacheInitializer, ProductStatusCacheInitializer>();


            return services;
        }
    }
}

