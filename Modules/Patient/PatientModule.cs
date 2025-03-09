using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PatientApi.Common.Common;
using PatientApi.Modules.Patient.Infrastructure;

namespace PatientApi.Modules.Patient.Patient;

public class PatientModule : IModule
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddPatientInfrastructure()
            .AddPatientModuleDbContext();
    }

    public IEnumerable<Assembly> GetAssemblies()
    {
        return
        [
            typeof(IPatientPointer).Assembly
        ];
    }
}