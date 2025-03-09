using PatientApi;
using PatientApi.Modules.Patient.Patient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterModule<PatientModule>();

var modules = builder.Services.AddRegisteredModules(builder.Configuration, builder.Environment);
foreach (var module in modules)
{
    module.ConfigureServices(builder.Services);
}

builder.Services.AddModuleControllers(modules);
builder.Services.AddModuleMediatRAssemblies(modules);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.Run();

public partial class Program
{
}