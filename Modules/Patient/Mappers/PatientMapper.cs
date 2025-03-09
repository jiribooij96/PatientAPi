using PatientApi.Modules.Patient.Patient.DateTransferObjects;
using Riok.Mapperly.Abstractions;

namespace PatientApi.Modules.Patient.Patient.Mappers;

[Mapper]
public partial class PatientMapper
{
    [MapperIgnoreSource(nameof(patient.CreatedAt))]
    [MapperIgnoreSource(nameof(patient.UpdatedAt))]
    public partial PatientDto PatientToPatientDto(Core.Entities.Patient patient);
}