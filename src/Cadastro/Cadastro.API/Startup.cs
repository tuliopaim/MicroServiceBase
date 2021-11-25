using Cadastro.Application;
using Cadastro.Domain.Repositories;
using Cadastro.Infrastructure.Context;
using Cadastro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MSBase.Core.API;

namespace Cadastro.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegistrarCore(new CoreSettings
            {
                HostEnvironment = _environment,
                Configuration = Configuration,
                TipoDaCamadaDeApplication = typeof(IApplicationAssemblyMarker),
                TipoDoStartup = typeof(Startup)
            });

            services.AddDbContext<AppDbContext>();

            services.AddScoped<IPessoaRepository, PessoaRepository>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cadastro.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                dbContext.Database.Migrate();                
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadatro.API v1"));
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
