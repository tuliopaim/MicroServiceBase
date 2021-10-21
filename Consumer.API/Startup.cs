using AuditoriaAPI.Infrasctructure;
using AuditoriaAPI.Infrasctructure.Context;
using CQRS.Core.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AuditoriaAPI
{
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Consumer.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consumer.API v1"));
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
}
