using PatientApi.Common.Common;
using PatientApi.Common.Common.Repositories;

namespace PatientApi.Modules.AuditLogs.Infrastructure.Repositories;

public interface IReadRepository<out T> : IBaseReadRepository<T>
    where T : class, IEntity
{
}
