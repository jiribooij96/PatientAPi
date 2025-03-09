using PatientApi.Common.Common;

namespace PatientApi.Modules.AuditLogs.Core.Entities;

public class AuditLog : BaseEntity
{
    public Guid PatientId {get; set;}
    public Guid UserId {get; set;}
    public DateTime CreatedOn {get; set;}
    public string CreatedBy {get; set;}
    public DateTime ModifiedOn {get; set;}
    public string ModifiedBy {get; set;}
    public string ModifiedEntity {get; set;}
}