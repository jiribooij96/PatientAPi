namespace PatientApi.Modules.Patient.Patient.DateTransferObjects;

public record PatientDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<PatientDocumentDto> Documents { get; } = [];

    public bool HasAllergies { get; set; }

    public List<AllergyDto>? Allergies { get; set; }
}