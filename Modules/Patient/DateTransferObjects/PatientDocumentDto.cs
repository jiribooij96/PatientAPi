using PatientApi.Modules.Patient.Core.Entities;

namespace PatientApi.Modules.Patient.Patient.DateTransferObjects;

public record PatientDocumentDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string DocumentName { get; set; }
    public DocumentType DocumentType { get; set; }
    public Stream Document { get; set; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset UpdatedAt { get; }
}