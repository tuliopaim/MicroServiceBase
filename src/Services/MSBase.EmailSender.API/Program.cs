using FluentEmail.MailKitSmtp;
using MSBase.Core.Extensions;
using MSBase.EmailSender.API.Consumers;
using MSBase.EmailSender.API.Domain;
using MSBase.EmailSender.API.Infrastructure;
using MSBase.EmailSender.Templates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services
    .AddCore(builder.Configuration)
    .AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>()
    .AddScoped<IEmailService, EmailService>()
    .AddScoped<IEmailSender, EmailSender>()
    .AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>()
    .AddHostedService<NovoEmailConsumerBackgroundService>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services
    .AddFluentEmail("msbase@gmail.com", "MSBase")
    .AddMailKitSender(new SmtpClientOptions
    {
        User = "084d75abdb31df",
        Password = "2a0cd517212549",
        Port = 2525,
        Server = "smtp.mailtrap.io",
        RequiresAuthentication = true,
        UseSsl = false,
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
