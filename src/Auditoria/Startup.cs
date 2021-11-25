using Auditoria.API.Infrasctructure;
using Core.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Auditoria.API;

public class Startup
{
    private readonly IHostEnvironment _hostEnvironment;
    public readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        _configuration = configuration;
        _hostEnvironment = hostEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.RegistrarCore(new CoreSettings
        {
            Configuration = _configuration,
            HostEnvironment = _hostEnvironment,
            TipoDaCamadaDeApplication = typeof(Startup),
            TipoDoStartup = typeof(Startup),
        });

        services.AddDbContext<AuditoriaDbContext>();

        services.AddScoped<IAuditoriaRepository, AuditoriaRepository>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auditoria.API", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AuditoriaDbContext dbContext)
    {
        if (env.IsDevelopment())
        {
            dbContext.Database.Migrate();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auditoria.API v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

