using PatientApi.Common.Common;
using PatientApi.Common.Common.Repositories;

namespace PatientApi.Modules.Patient.Infrastructure.Repositories;

public class ReadRepository<T>(PatientContext context)
    : BaseReadRepository<T, PatientContext>(context), IReadRepository<T>
    where T : class, IEntity;
