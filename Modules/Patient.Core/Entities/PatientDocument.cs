using PatientApi.Common.Common;

namespace PatientApi.Modules.Patient.Core.Entities;

public class PatientDocument : BaseEntity
{
    public Guid PatientId { get; set; }
    public virtual Patient Patient { get; set; }
    public string DocumentName { get; set; }
    public DocumentType DocumentType { get; set; }
    public byte[] DocumentContent { get; set; } // Cloud storage is better with an url
}