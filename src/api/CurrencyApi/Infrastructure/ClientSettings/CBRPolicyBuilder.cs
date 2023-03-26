using CurrencyApi.Infrastructure.CBRPolicySettings;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Timeout;
using System.Runtime.CompilerServices;

namespace CurrencyApi.Infrastructure.ClientSettings;

public static class CBRPolicyBuilder
{
    public static IHttpClientBuilder AddRetryPolicy(
        this IHttpClientBuilder builder, CBRRetryPolicySettings retrySettings)
    {
        var settings = new CBRRetrySettingsHandler(retrySettings);

        return builder.AddPolicyHandler(
            HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(
                settings.RetryCount,
                settings.SleepDurationProvider,
                settings.OnRetry));
    }
}
