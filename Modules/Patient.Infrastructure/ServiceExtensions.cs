using Microsoft.Extensions.DependencyInjection;
using PatientApi.Common.Common;
using PatientApi.Modules.Patient.Infrastructure.Repositories;

namespace PatientApi.Modules.Patient.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddPatientInfrastructure(
        this IServiceCollection services)
    {
        services.Scan(x =>
        {
            x
                .FromAssemblyOf<IPatientInfrastructurePointer>()
                .AddClasses(classes =>
                    classes.AssignableToAny(typeof(IWriteRepository<>),
                        typeof(IReadRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
        
        return services;
    }

    public static IServiceCollection AddPatientModuleDbContext(
        this IServiceCollection services)
    {
        return services.AddModuleDbContext<PatientContext>();
    }
}
