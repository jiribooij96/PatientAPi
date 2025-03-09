namespace PatientApi.Modules.Patient.Contracts.Events;

public record PatientCreatedDomainEvent(
    Guid PatientId,
    Guid UserId,
    DateTime CreatedOn,
    string CreatedBy,
    DateTime ModifiedOn,
    string ModifiedBy,
    string ModifiedEntity);