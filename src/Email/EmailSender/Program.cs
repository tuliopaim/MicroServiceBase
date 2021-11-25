using Core.API;
using EmailSender.Domain;
using EmailSender.Infrastructure;
using EmailSender.Templates;
using FluentEmail.MailKitSmtp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailSender, EmailSender.Infrastructure.EmailSender>();

builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

builder.Services.AddFluentEmail("msbase@gmail.com", "MSBase")
    .AddMailKitSender(new SmtpClientOptions{
        User = "084d75abdb31df",
        Password = "2a0cd517212549",
        Port = 2525,
        Server = "smtp.mailtrap.io",
        RequiresAuthentication = true,
        UseSsl = false,
    });

builder.Services.RegistrarCore(new CoreSettings
{
    Configuration = builder.Configuration,
    HostEnvironment = builder.Environment,
    TipoDaCamadaDeApplication = typeof(Startup),
    TipoDoStartup = typeof(Startup),
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
