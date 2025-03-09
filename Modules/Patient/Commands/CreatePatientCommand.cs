using MediatR;
using PatientApi.Modules.Patient.Infrastructure.Repositories;
using PatientApi.Modules.Patient.Patient.Exceptions;

namespace PatientApi.Modules.Patient.Patient.Commands;

public record CreatePatientCommand(string FirstName, string LastName, string Email) : IRequest<Guid>;

public class CreatePatientCommandHandler(IWriteRepository<Core.Entities.Patient> writeRepository)
    : IRequestHandler<CreatePatientCommand, Guid>
{
    public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var existingPatient = await writeRepository.GetAsync(x => x.Email == request.Email, cancellationToken);

        if (existingPatient != null)
            throw new EntityAlreadyExistsException($"Patient with email '{request.Email}' already exists");

        var patient = new Core.Entities.Patient(request.FirstName, request.LastName, request.Email);
        patient.Id = Guid.NewGuid();
        
        writeRepository.Add(patient);
        await writeRepository.SaveEntitiesAsync(cancellationToken);

        return patient.Id;
    }
}