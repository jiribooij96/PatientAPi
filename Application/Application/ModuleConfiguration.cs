using PatientApi.Common.Common;

namespace PatientApi;

public static class ModuleConfiguration
{
    public static IModule[] AddRegisteredModules(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        var modules = services.BuildServiceProvider().GetRequiredService<IEnumerable<IModule>>().ToArray();

        foreach (var module in modules) module.ConfigureServices(services);

        return modules;
    }
}