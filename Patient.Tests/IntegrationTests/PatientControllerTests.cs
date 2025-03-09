using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PatientApi.Modules.Patient.Infrastructure;
using PatientApi.Modules.Patient.Patient.DateTransferObjects;
using PatientApi.Modules.Patient.Patient.Requests;
using Xunit;

namespace PatientApi.Modules.Patient.Tests.IntegrationTests;

public class PatientControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly PatientContext _context = factory.Services.GetRequiredService<PatientContext>();

    [Fact]
    public async Task GetPatient_ReturnsOk_WhenPatientExists()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patient = new Core.Entities.Patient("John", "Doe", "johndoe@gmail.com")
        {
            Id = patientId
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        var response = await _client.GetAsync($"/api/patient/{patientId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var patientResult = await response.Content.ReadFromJsonAsync<PatientDto>();
        patientResult.Should().NotBeNull();
        patientResult!.Id.Should().Be(patientId);
        patientResult!.FirstName.Should().Be("John");
        patientResult!.LastName.Should().Be("Doe");
        patientResult!.Email.Should().Be("johndoe@gmail.com");
    }

    [Fact]
    public async Task CreatePatient_ReturnsCreated_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreatesPatientRequest("John", "Doe", "johndoe@gmail.com", false, null)
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/patient/create", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task AddDocument_ReturnsOk_WhenFileIsValid()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var patient = new Core.Entities.Patient("John", "Doe", "johndoe@gmail.com")
        {
            Id = patientId
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var content = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent([1, 2, 3, 4]);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        content.Add(fileContent, "file", "test.pdf");

        // Act
        var response = await _client.PutAsync($"/api/patient/{patientId}/addDocument", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}