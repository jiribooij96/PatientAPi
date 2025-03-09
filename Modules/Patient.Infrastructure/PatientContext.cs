using Microsoft.EntityFrameworkCore;
using PatientApi.Common.Common;
using PatientApi.Modules.Patient.Core.Entities;

namespace PatientApi.Modules.Patient.Infrastructure;

public class PatientContext(DbContextOptions<PatientContext> options) : ModuleDbContext(options)

{
    public DbSet<Core.Entities.Patient> Patients { get; set; }
    public DbSet<Allergy> Allergies { get; set; }
}