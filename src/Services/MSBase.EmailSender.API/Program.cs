using FluentEmail.MailKitSmtp;
using MSBase.Core.API;
using MSBase.EmailSender.API.Domain;
using MSBase.EmailSender.API.Infrastructure;
using MSBase.EmailSender.Templates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();

builder.Services.AddFluentEmail("msbase@gmail.com", "MSBase")
    .AddMailKitSender(new SmtpClientOptions
    {
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
    TipoDaCamadaDeApplication = typeof(Program),
    TipoDoStartup = typeof(Program),
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
