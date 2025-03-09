using PatientApi.Common.Common;
using PatientApi.Common.Common.Repositories;

namespace PatientApi.Modules.Patient.Infrastructure.Repositories;

public class WriteRepository<T>(PatientContext context)
    : BaseWriteRepository<T, PatientContext>(context), IWriteRepository<T>
    where T : class, IEntity;
