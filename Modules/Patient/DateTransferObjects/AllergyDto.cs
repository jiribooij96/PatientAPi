namespace PatientApi.Modules.Patient.Patient.DateTransferObjects;

public record AllergyDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public int AllergyCode { get; set; }
    public string AllergyDescription { get; set; }
    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset UpdatedAt { get; }
}