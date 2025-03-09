using MediatR;
using Microsoft.EntityFrameworkCore;
using PatientApi.Modules.Patient.Infrastructure.Repositories;
using PatientApi.Modules.Patient.Patient.DateTransferObjects;
using PatientApi.Modules.Patient.Patient.Exceptions;
using PatientApi.Modules.Patient.Patient.Mappers;

namespace PatientApi.Modules.Patient.Patient.Queries;

public record GetPatientByIdQuery(Guid Id) : IRequest<PatientDto>;

public class GetPatientQueryHandler(IReadRepository<Core.Entities.Patient> readRepository)
    : IRequestHandler<GetPatientByIdQuery, PatientDto>
{
    private readonly PatientMapper _mapper = new();

    public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await readRepository.Query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                      ?? throw new EntityNotFoundException($"Patient with Id '{request.Id}' not found");


        // ToDo Add AuditLogs consumer
        // bus.Publish(new PatientViewedEvent(patient.Id));

        return _mapper.PatientToPatientDto(patient);
    }
}