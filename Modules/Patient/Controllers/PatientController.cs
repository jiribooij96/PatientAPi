using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientApi.Modules.Patient.Patient.Commands;
using PatientApi.Modules.Patient.Patient.DateTransferObjects;
using PatientApi.Modules.Patient.Patient.Exceptions;
using PatientApi.Modules.Patient.Patient.Queries;
using PatientApi.Modules.Patient.Patient.Requests;

namespace PatientApi.Modules.Patient.Patient.Controllers;

[ApiController]
[Route("api/patient")]
public class PatientController(IMediator mediator) : ControllerBase
{
    [HttpGet("{patientId:guid}")]
    [ProducesResponseType(typeof(ActionResult<PatientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PatientDto>> GetPatient(Guid patientId, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetPatientByIdQuery(patientId);
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePatient([FromBody] CreatesPatientRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreatePatientCommand(request.FirstName, request.LastName, request.Email);
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result);
        }
        catch (EntityAlreadyExistsException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{patientId:guid}/addDocument")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddDocument(Guid patientId, [FromBody] IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file.Length == 0) return BadRequest("No file was provided or the file is empty.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        string[] allowedExtensions = { ".pdf", ".docx", ".doc", ".txt" }; // Add/modify based on your requirements

        if (!allowedExtensions.Contains(extension))
            return BadRequest("File type not supported. Allowed types: " + string.Join(", ", allowedExtensions));

        try
        {
            var command = new AddPatientDocumentCommand(patientId, file.OpenReadStream());
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result);
        }
        catch (EntityNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}