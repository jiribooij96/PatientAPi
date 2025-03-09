using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PatientApi.Common.Common;

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services) where T : ModuleDbContext
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }

    public static IServiceCollection AddModuleDbContext<T>(this IServiceCollection services) where T : ModuleDbContext
    {
        services.RemoveDbContext<T>();

        return services.AddDbContext<T>(x =>
        {
           x.UseInMemoryDatabase(nameof(T));
        });
    }
}
