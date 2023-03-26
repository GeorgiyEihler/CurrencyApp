using Currency.Contract.LogManager;
using Currency.Logger;
using CurrencyApi.Infrastructure.CBRPolicySettings;
using CurrencyApi.Infrastructure.ClientSettings;

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

        var policySettings = configuration.GetSection("PolicySettings").Get<CBRRetryPolicySettings>();

        if (policySettings is null)
        {
            throw new ArgumentNullException(nameof(policySettings));
        }

        var cbrClientSettings = clientSection?.Where(c => c.Name == ClientConstants.CBRClient).FirstOrDefault();

        if (cbrClientSettings is not null)
        {
            services.AddHttpClient(ClientConstants.CBRClient, httpClient =>
                httpClient.BaseAddress = new Uri(cbrClientSettings.BaseUri))
                .AddRetryPolicy(policySettings);
        }

        var cbrDailySettings = clientSection?.Where(c => c.Name == ClientConstants.CBRDailyClient).FirstOrDefault();

        if (cbrDailySettings is not null)
        {
            services.AddHttpClient(ClientConstants.CBRDailyClient, httpClient =>
                httpClient.BaseAddress = new Uri(cbrDailySettings.BaseUri))
                .AddRetryPolicy(policySettings);
        }
    }


    public static void ConfigureLogging(this IServiceCollection services) =>
        services.AddSingleton<ILoggingManager, CurrencyLogManager>();
}
