using MSBase.Cadastro.API.Commands.NovaPessoaCommand;
using MSBase.Cadastro.API.Infrastructure.Context;
using MSBase.Cadastro.API.Infrastructure.Repositories;
using MSBase.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddCore(builder.Configuration, config =>
    {
        config.WithCqrs(typeof(NovaPessoaCommandHandler).Assembly);
    })
    .AddDbContext<AppDbContext>()
    .AddScoped<IPessoaRepository, PessoaRepository>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadatro.API v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();