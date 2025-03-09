using System.Text.Json.Serialization;
using PatientApi.Common.Common;

namespace PatientApi;

public static class ServiceExtensions
{
    public static void RegisterModule<TModule>(this IServiceCollection services) where TModule : class, IModule
    {
        services.AddSingleton<IModule, TModule>();
    }

    public static void AddModuleControllers(this IServiceCollection services, IEnumerable<IModule> modules)
    {
        var controllerBuilder = services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        var applicationParts = modules.SelectMany(x => x.GetAssemblies());

        foreach (var applicationPart in applicationParts)
        {
            controllerBuilder.AddApplicationPart(applicationPart);
        }
    }
    
    public static void AddModuleMediatRAssemblies(this IServiceCollection services, IEnumerable<IModule> modules)
    {
        var mediatRAssemblies = modules.SelectMany(x => x.GetAssemblies()).ToArray();

        if (mediatRAssemblies.Length == 0)
        {
            return;
        }

        services.AddMediatR(config => { config.RegisterServicesFromAssemblies(mediatRAssemblies); });
    }
}
