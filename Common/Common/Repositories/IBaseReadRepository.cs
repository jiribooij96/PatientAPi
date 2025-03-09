namespace PatientApi.Common.Common.Repositories;

public interface IBaseReadRepository<out T>
    where T : class, IEntity
{
    IQueryable<T> Query { get; }
}
