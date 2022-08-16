using Business.Interface;
using Business.Services;
using FluentValidation;
using Models.Mapper.Request;

namespace WebApi.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IClientService, ClientService>();

            //Fluent Validation
            services.AddTransient<IValidator<ClientPost>, ClientPostValidation>();

            return services;
        }
    }
}
