using PatientApi.Common.Common;
using PatientApi.Common.Common.Repositories;

namespace PatientApi.Modules.AuditLogs.Infrastructure.Repositories;

public class ReadRepository<T>(AuditLogContext context)
    : BaseReadRepository<T, AuditLogContext>(context), IReadRepository<T>
    where T : class, IEntity;
