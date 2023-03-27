using CurrencyApi.Extention;
using CurrencyApi.Infrastructure.ClientSettings;
using CurrencyApi.Presentation.AssamblyMarker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NLog;
using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), $"/LoggerSettings/nlog.{environment}.config"));


builder.Services.ConfigureLogging();

builder.Services.ConfigureCBRClients(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(IPresentationAssamplyMarker).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
