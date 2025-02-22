using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
  
 
namespace IDSLatam.Service.Geonodo.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
 
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                // cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });
 
            return services;
        }
    }
}
