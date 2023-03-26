using Currency.Contract.LogManager;
using CurrencyApi.Infrastructure.CBRPolicySettings;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace CurrencyApi.Infrastructure.ClientSettings;

public static class CBRPolicyBuilder
{
    public static IHttpClientBuilder AddRetryPolicy(
        this IHttpClientBuilder builder, CBRRetryPolicySettings retrySettings)
    {
        var settings = new CBRRetrySettingsHandler(retrySettings);

        return builder.AddPolicyHandler((services, request) =>
            HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(
                settings.RetryCount,
                settings.SleepDurationProvider,
                (result, timeSpan, retryCount, context) => {
                    var logger = services.GetRequiredService<ILoggingManager>();
                    settings.OnRetry(result, timeSpan, retryCount, context, logger);    
                }));
    }
}
