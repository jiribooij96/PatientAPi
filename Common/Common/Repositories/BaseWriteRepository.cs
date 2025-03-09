using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PatientApi.Common.Common.Repositories;

public class BaseWriteRepository<T, TContext> : IBaseWriteRepository<T>
    where T : class, IEntity
    where TContext : ModuleDbContext
{
    private TContext Context { get; }
    protected virtual DbSet<T> DbSet => Context.Set<T>();
    protected virtual IQueryable<T> Query =>
        DbSet.OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Id);
    protected virtual IQueryable<T> GetQuery => Query;
    
    protected BaseWriteRepository(TContext context)
    {
        Context = context;
    }
    
    public async Task<T?> GetAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        var query = GetQuery;

        query = query.Where(filter);

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void AddRange(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }
    
    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return Context.SaveChangesAsync(cancellationToken);
    }
}