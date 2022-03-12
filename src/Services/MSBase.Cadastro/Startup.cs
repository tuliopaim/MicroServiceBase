using MSBase.Cadastro.API.Commands.NovaPessoaCommand;
using MSBase.Cadastro.API.Entities;
using MSBase.Cadastro.API.Infrastructure.Context;
using MSBase.Cadastro.API.Infrastructure.Repositories;
using MSBase.Core.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCore(builder.Configuration, typeof(NovaPessoaCommandHandler).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadatro.API v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();