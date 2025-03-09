using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientApi.Common.Common;
using PatientApi.Modules.Patient.Core.Entities;

namespace PatientApi.Modules.Patient.Infrastructure.EntityConfigurations;

public class AllergyConfiguration : BaseEntityTypeConfiguration<Allergy>
{
    public override void Configure(EntityTypeBuilder<Allergy> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.AllergyCode).IsRequired();
        builder.Property(x => x.AllergyDescription).IsRequired();
    }
}