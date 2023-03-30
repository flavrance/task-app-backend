using Microsoft.Extensions.DependencyInjection;
using TaskApp.Application.Commands.Register;
using TaskApp.Application.Commands.Task;

namespace TaskApp.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUseCase, RegisterUseCase>();
            services.AddScoped<ITaskUseCase, TaskUseCase>();

            return services;
        }
    }
}
