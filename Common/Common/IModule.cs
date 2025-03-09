using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PatientApi.Common.Common;

public interface IModule
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Use for global configuration
    }
    
    public IEnumerable<Assembly> GetAssemblies()
    {
        return ArraySegment<Assembly>.Empty;
    }
}
