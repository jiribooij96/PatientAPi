using Microsoft.EntityFrameworkCore;

namespace PatientApi.Common.Common;

public abstract class ModuleDbContext(DbContextOptions options) : DbContext(options)
{
    // Register any common configurations here
}
