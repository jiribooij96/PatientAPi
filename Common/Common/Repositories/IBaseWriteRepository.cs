using System.Linq.Expressions;

namespace PatientApi.Common.Common.Repositories;

public interface IBaseWriteRepository<T>
    where T : class, IEntity
{
    Task<T?> GetAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
    
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    void Update(T entity);

    void Add(T entity);

    void AddRange(IEnumerable<T> entities);

    void Delete(T entity);
    
    Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
