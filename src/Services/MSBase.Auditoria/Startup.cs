using Microsoft.OpenApi.Models;
using MSBase.Auditoria.API.Infrasctructure;
using MSBase.Core.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuditoriaDbContext>();
builder.Services.AddScoped<IAuditoriaRepository, AuditoriaRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auditoria.API", Version = "v1" });
});

builder.Services.RegistrarCore(new CoreSettings
{
    Configuration = builder.Configuration,
    HostEnvironment = builder.Environment,
    TipoDaCamadaDeApplication = typeof(MSBase.Auditoria.API.Domain.Auditoria),
    TipoDoStartup = typeof(MSBase.Auditoria.API.Domain.Auditoria),
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auditoria.API v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
