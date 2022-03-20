using Microsoft.OpenApi.Models;
using MSBase.Auditoria.API.Commands.NovaAuditoriaCommand;
using MSBase.Auditoria.API.Consumers;
using MSBase.Auditoria.API.Infrasctructure;
using MSBase.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddCore(builder.Configuration, config =>
    {
        config.WithCqrs(typeof(NovaAuditoriaCommandHandler).Assembly);
    })
    .AddDbContext<AuditoriaDbContext>()
    .AddScoped<IAuditoriaRepository, AuditoriaEntidadeRepository>()
    .AddHostedService<AuditoriaConsumerBackgroundService>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuditoriaEntidade.API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuditoriaEntidade.API v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
