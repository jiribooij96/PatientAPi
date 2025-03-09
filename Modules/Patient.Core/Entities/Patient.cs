using PatientApi.Common.Common;

namespace PatientApi.Modules.Patient.Core.Entities;

public class Patient : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<PatientDocument> Documents { get; } = [];

    public bool HasAllergies { get; set; }

    public List<Allergy>? Allergies { get; set; }
    
    public Patient(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;

        // ToDo create for audit logs
        // AddDomainEvent(new PatientCreatedDomainEvent(this));
    }

    public void AddDocument(PatientDocument document)
    {
        Documents.Add(document);

        // ToDo create for audit logs
        // AddDomainEvent(new PatientDocumentAddedDomainEvent(this));
    }

    public void RemoveDocument(PatientDocument document)
    {
        Documents.Remove(document);

        // ToDo create for audit logs
        // AddDomainEvent(new PatientDocumentRemovedDomainEvent(this));
    }
}