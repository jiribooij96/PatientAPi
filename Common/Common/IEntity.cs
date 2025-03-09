namespace PatientApi.Common.Common;

public interface IEntity
{
    Guid Id { get; }

    DateTimeOffset CreatedAt { get; }

    DateTimeOffset UpdatedAt { get; }
}