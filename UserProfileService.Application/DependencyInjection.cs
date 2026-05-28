using Microsoft.Extensions.DependencyInjection;
using UserProfileService.Application.Services;

namespace UserProfileService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UserProfileAppService>();

        return services;
    }
}
