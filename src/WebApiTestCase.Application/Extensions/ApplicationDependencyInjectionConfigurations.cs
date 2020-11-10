using Microsoft.Extensions.DependencyInjection;
using WebApiTestCase.Application.Services.Task;
using WebApiTestCase.Application.Services.User;

namespace WebApiTestCase.Application.Extensions
{
    public static class ApplicationDependencyInjectionConfigurations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}