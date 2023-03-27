using Currency.Contract.Context;
using Currency.Contract.CurrencyService;
using Currency.Contract.LogManager;
using Currency.Logger;
using Currency.Repository.CurrencyRepositoy;
using Currency.Repository.DapperContext;
using Currency.Service.CBRCurrencyService;
using CurrencyApi.Infrastructure.CBRPolicySettings;
using CurrencyApi.Infrastructure.ClientPolicyBuilder;
using CurrencyApi.Infrastructure.ClientSettings;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Net.Http;

namespace CurrencyApi.Extention;

public static class ServiceExtensions
{
    /// <summary>
    /// Adding CBR clients.
    /// </summary>
    /// <param name="services">DI container.</param>
    /// <param name="configuration">App configuration.</param>
    public static void ConfigureCBRClients(this IServiceCollection services, IConfiguration configuration)
    {
        var clientSection = configuration.GetSection("HttpClient").Get<HttpClientSettings[]>();

        var retryPolicySettings = configuration.GetSection("RetryPolicySettings").Get<CBRRetryPolicySettings>();

        var circuitBreakerPolicySettings = configuration.GetSection("CircuitBreakerPolicySettings").Get<CBRCircuitBreakerPolicySettings>();

        if (retryPolicySettings is null)
        {
            throw new ArgumentNullException(nameof(retryPolicySettings));
        }

        if (circuitBreakerPolicySettings is null)
        {
            throw new ArgumentNullException(nameof(circuitBreakerPolicySettings));
        }

        var cbrClientSettings = clientSection?.Where(c => c.Name == ClientConstants.CBRClient).FirstOrDefault();

        if (cbrClientSettings is not null)
        {
            services.AddHttpClient(ClientConstants.CBRClient, httpClient =>
                {
                    httpClient.BaseAddress = new Uri(cbrClientSettings.BaseUri);
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/xml");
                })
                .AddRetryPolicy(retryPolicySettings)
                .AddCircuitBreakerPolicy(circuitBreakerPolicySettings);
        }

        var cbrDailySettings = clientSection?.Where(c => c.Name == ClientConstants.CBRDailyClient).FirstOrDefault();

        if (cbrDailySettings is not null)
        {
            services.AddHttpClient(ClientConstants.CBRDailyClient, httpClient =>
                {
                    httpClient.BaseAddress = new Uri(cbrDailySettings.BaseUri);
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .AddRetryPolicy(retryPolicySettings)
                .AddCircuitBreakerPolicy(circuitBreakerPolicySettings);
        }
    }

    /// <summary>
    /// Configure logging service.
    /// </summary>
    /// <param name="services">Service collection.</param>
    public static void ConfigureLogging(this IServiceCollection services) =>
        services.AddSingleton<ILoggingManager, CurrencyLogManager>();

    public static void ConfigureDbContext(this IServiceCollection services) =>
        services.AddSingleton<ICurrencyContext, CurrencyDbContext>();

    public static void ConfigureRepositoryes(this IServiceCollection services) =>
        services.AddScoped<ICurrencyRepository, CerrencyRepository>();

    public static void ConfigureCurrencyService(this IServiceCollection services) =>
        services.AddScoped<ICurrencyService, CBRService>();
}
