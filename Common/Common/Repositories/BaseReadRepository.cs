using Microsoft.EntityFrameworkCore;

namespace PatientApi.Common.Common.Repositories;

public class BaseReadRepository<T, TContext>(TContext context) : IBaseReadRepository<T>
    where T : class, IEntity
    where TContext : ModuleDbContext
{
    protected TContext Context { get; } = context;

    protected virtual DbSet<T> DbSet => Context.Set<T>();

    public virtual IQueryable<T> Query =>
        DbSet
            .OrderByDescending(x => x.CreatedAt)
            .ThenBy(x => x.Id)
            .AsNoTracking();
}