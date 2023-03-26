using CurrencyApi.Extention;
using CurrencyApi.Infrastructure.ClientSettings;
using CurrencyApi.Presentation.AssamblyMarker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

ServiceExtensions.ConfigureCBRClients(builder.Services, builder.Configuration);

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
