using PatientApi.Common.Common;

namespace PatientApi.Modules.Patient.Core.Entities;

public class Allergy(Guid patientId, int allergyCode, string allergyDescription) : BaseEntity
{
    public Guid PatientId { get; set; } = patientId;
    public virtual Patient Patient { get; set; }
    public int AllergyCode { get; set; } = allergyCode;
    public string AllergyDescription { get; set; } = allergyDescription;
}