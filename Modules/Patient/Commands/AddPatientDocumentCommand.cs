using MediatR;
using PatientApi.Modules.Patient.Core.Entities;
using PatientApi.Modules.Patient.Infrastructure.Repositories;
using PatientApi.Modules.Patient.Patient.Exceptions;

namespace PatientApi.Modules.Patient.Patient.Commands;

public record AddPatientDocumentCommand(Guid PatientId, Stream Document) : IRequest<Guid>;

public class AddDocumentCommandHandler(IWriteRepository<Core.Entities.Patient> writeRepository)
    : IRequestHandler<AddPatientDocumentCommand, Guid>
{
    public async Task<Guid> Handle(AddPatientDocumentCommand request, CancellationToken cancellationToken)
    {
        var existingPatient = await writeRepository.GetByIdAsync(request.PatientId, cancellationToken)
                              ?? throw new EntityNotFoundException($"Patient with Id '{request.PatientId}' not found");

        existingPatient.AddDocument(new PatientDocument
        {
            PatientId = request.PatientId,
            DocumentContent = ConvertStreamToByteArray(request.Document),
            DocumentName = "Medical Record",
            DocumentType = DocumentType.MedicalRecord
        });

        writeRepository.Update(existingPatient);
        await writeRepository.SaveEntitiesAsync(cancellationToken);

        return existingPatient.Id;
    }

    private static byte[] ConvertStreamToByteArray(Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}