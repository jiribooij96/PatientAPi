namespace PatientApi.Modules.Patient.Patient.Requests;

public record CreatesPatientRequest(
    string FirstName,
    string LastName,
    string Email,
    bool HasAllergies,
    List<AllergiesDto>? Allergies);

public record AllergiesDto(
    int AllergieCode,
    string AllergiesDescription
);