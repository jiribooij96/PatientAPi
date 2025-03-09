using System.Linq.Expressions;
using Moq;
using PatientApi.Modules.Patient.Infrastructure.Repositories;
using PatientApi.Modules.Patient.Patient.Commands;
using PatientApi.Modules.Patient.Patient.Exceptions;
using Xunit;

namespace PatientApi.Modules.Patient.Tests.UnitTests;

public class CreatePatientCommandHandlerTests
{
    private readonly Mock<IWriteRepository<Core.Entities.Patient>> _mockRepository;
    private readonly CreatePatientCommandHandler _commandHandler;

    public CreatePatientCommandHandlerTests()
    {
        _mockRepository = new Mock<IWriteRepository<Core.Entities.Patient>>();
        _commandHandler = new CreatePatientCommandHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreatePatient_WhenPatientDoesNotExist()
    {
        // Arrange
        var command = new CreatePatientCommand("John", "Doe", "john.doe@example.com");
        _mockRepository.Setup(repo =>
                repo.GetAsync(It.IsAny<Expression<Func<Core.Entities.Patient, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.Patient)null);

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(repo => repo.Add(It.IsAny<Core.Entities.Patient>()), Times.Once);
        _mockRepository.Verify(repo => repo.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenPatientAlreadyExists()
    {
        // Arrange
        var command = new CreatePatientCommand("John", "Doe", "john.doe@example.com");
        var existingPatient = new Core.Entities.Patient("John", "Doe", "john.doe@example.com");
        _mockRepository.Setup(repo =>
                repo.GetAsync(It.IsAny<Expression<Func<Core.Entities.Patient, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingPatient);

        // Act & Assert
        await Assert.ThrowsAsync<EntityAlreadyExistsException>(() =>
            _commandHandler.Handle(command, CancellationToken.None));
        _mockRepository.Verify(repo => repo.Add(It.IsAny<Core.Entities.Patient>()), Times.Never);
        _mockRepository.Verify(repo => repo.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}