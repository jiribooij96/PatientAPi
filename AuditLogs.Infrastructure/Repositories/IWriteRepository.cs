using PatientApi.Common.Common;
using PatientApi.Common.Common.Repositories;

namespace PatientApi.Modules.AuditLogs.Infrastructure.Repositories;

public interface IWriteRepository<T> : IBaseWriteRepository<T>
    where T : class, IEntity
{
}

