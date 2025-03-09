using PatientApi.Common.Common;
using PatientApi.Common.Common.Repositories;

namespace PatientApi.Modules.AuditLogs.Infrastructure.Repositories;

public class WriteRepository<T>(AuditLogContext context)
    : BaseWriteRepository<T, AuditLogContext>(context), IWriteRepository<T>
    where T : class, IEntity;
