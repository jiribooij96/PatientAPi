using Microsoft.EntityFrameworkCore;
using PatientApi.Common.Common;
using PatientApi.Modules.AuditLogs.Core.Entities;

namespace PatientApi.Modules.AuditLogs.Infrastructure;

public class AuditLogContext(DbContextOptions<AuditLogContext> options) : ModuleDbContext(options)
{
    public DbSet<AuditLog> AuditLogs { get; set; }
}