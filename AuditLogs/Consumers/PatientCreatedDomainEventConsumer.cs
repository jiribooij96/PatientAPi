using MassTransit;
using PatientApi.Modules.AuditLogs.Core.Entities;
using PatientApi.Modules.AuditLogs.Infrastructure.Repositories;
using PatientApi.Modules.Patient.Contracts.Events;

namespace PatientApi.Modules.AuditLogs.AuditLogs.Consumers;

public class PatientCreatedDomainEventConsumer(IWriteRepository<AuditLog> writeRepository) : IConsumer<PatientCreatedDomainEvent>
{
    public async Task Consume(ConsumeContext<PatientCreatedDomainEvent> context)
    {
        var auditLog = new AuditLog
        {
            CreatedBy = context.Message.CreatedBy,
            CreatedOn = context.Message.CreatedOn,
            ModifiedBy = context.Message.ModifiedBy,
            ModifiedOn = context.Message.ModifiedOn,
            UserId = context.Message.UserId,
        };
        
        writeRepository.Add(auditLog);
        await writeRepository.SaveEntitiesAsync();
    }
}