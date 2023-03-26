using Currency.Contract.LogManager;
using CurrencyApi.Infrastructure.CBRPolicyHandler;
using CurrencyApi.Infrastructure.CBRPolicySettings;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace CurrencyApi.Infrastructure.ClientPolicyBuilder;

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
                (result, timeSpan, retryCount, context) =>
                {
                    var logger = services.GetRequiredService<ILoggingManager>();
                    settings.OnRetry(result, timeSpan, retryCount, context, logger);
                }));
    }

    public static IHttpClientBuilder AddCircuitBreakerPolicy(
       this IHttpClientBuilder builder, CBRCircuitBreakerPolicySettings circuitBreakerSettings)
    {
        var settings = new CBRCircuitBreakerPolicyHandler(circuitBreakerSettings);

        return builder.AddPolicyHandler((services, request) =>
            HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .AdvancedCircuitBreakerAsync(
                settings.FailureThreshold,
                settings.SamplingDuration,
                settings.MinimumThroughput,
                settings.DurationOfBreak,
                (result, timeSpan) =>
                {
                    var logger = services.GetRequiredService<ILoggingManager>();
                    settings.OnBreak(result, timeSpan, logger);
                },
                () =>
                {
                    var logger = services.GetRequiredService<ILoggingManager>();
                    settings.OnReset(logger);
                },
                () =>
                {
                    var logger = services.GetRequiredService<ILoggingManager>();
                    settings.OnHalfOpen(logger);
                }));
    }
}
