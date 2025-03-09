using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientApi.Common.Common;

namespace PatientApi.Modules.Patient.Infrastructure.EntityConfigurations;

public class PatientConfiguration : BaseEntityTypeConfiguration<Core.Entities.Patient>
{
    public override void Configure(EntityTypeBuilder<Core.Entities.Patient> builder)
    {
        base.Configure(builder);

        builder.HasMany(x => x.Allergies)
            .WithOne(a => a.Patient)
            .HasForeignKey(x => x.PatientId);

        builder.HasMany(x => x.Documents)
            .WithOne(a => a.Patient)
            .HasForeignKey(x => x.PatientId);
    }
}