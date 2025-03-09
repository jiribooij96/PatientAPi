using Moq;
using PatientApi.Modules.Patient.Infrastructure.Repositories;
using PatientApi.Modules.Patient.Patient.Commands;
using PatientApi.Modules.Patient.Patient.Exceptions;
using Xunit;

namespace PatientApi.Modules.Patient.Tests.UnitTests;

public class AddDocumentCommandHandlerTests
{
    private readonly Mock<IWriteRepository<Core.Entities.Patient>> _mockRepository;
    private readonly AddDocumentCommandHandler _handler;

    public AddDocumentCommandHandlerTests()
    {
        _mockRepository = new Mock<IWriteRepository<Core.Entities.Patient>>();
        _handler = new AddDocumentCommandHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldAddDocument_WhenPatientExists()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var command = new AddPatientDocumentCommand(patientId, new MemoryStream(new byte[] { 1, 2, 3 }));
        var existingPatient = new Core.Entities.Patient("John", "Doe", "john.doe@example.com") { Id = patientId };
        _mockRepository.Setup(repo => repo.GetByIdAsync(patientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingPatient);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(repo => repo.Update(It.IsAny<Core.Entities.Patient>()), Times.Once);
        _mockRepository.Verify(repo => repo.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(patientId, result);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenPatientDoesNotExist()
    {
        // Arrange
        var patientId = Guid.NewGuid();
        var command = new AddPatientDocumentCommand(patientId, new MemoryStream(new byte[] { 1, 2, 3 }));
        _mockRepository.Setup(repo => repo.GetByIdAsync(patientId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.Patient)null);

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        _mockRepository.Verify(repo => repo.Update(It.IsAny<Core.Entities.Patient>()), Times.Never);
        _mockRepository.Verify(repo => repo.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}