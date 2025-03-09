using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientApi.Common.Common;
using PatientApi.Modules.Patient.Core.Entities;

namespace PatientApi.Modules.Patient.Infrastructure.EntityConfigurations;

public class PatientDocumentConfiguration : BaseEntityTypeConfiguration<PatientDocument>
{
    public override void Configure(EntityTypeBuilder<PatientDocument> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.DocumentContent).IsRequired();
        builder.Property(x => x.DocumentName).IsRequired();
        builder.Property(x => x.DocumentType).IsRequired();
    }
}