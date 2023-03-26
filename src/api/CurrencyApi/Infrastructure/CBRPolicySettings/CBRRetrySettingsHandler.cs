using Currency.Contract.LogManager;
using Microsoft.Extensions.Logging;
using NLog;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace CurrencyApi.Infrastructure.CBRPolicySettings;

/// <summary>
/// CBRRetryBaseSettings.
/// </summary>
public class CBRRetrySettingsHandler : IRetrySettingsHandler
{
    private readonly TimeSpan[] _delay;
    public CBRRetrySettingsHandler(CBRRetryPolicySettings retrySettings)
    {
        _delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: retrySettings.MedianFirstRetryDelay, 
            retryCount: retrySettings.RetryCount)
            .ToArray();

        RetryCount = retrySettings.RetryCount;
    }

    /// <summary>
    /// Retry count.
    /// </summary>
    public int RetryCount { get; }

    /// <summary>
    /// Sleep between requests.
    /// </summary>
    public TimeSpan SleepDurationProvider(int retryCount)
    {
        return _delay[retryCount - 1];
    }


    /// <summary>
    /// On retry strategy (Logger strategy).
    /// </summary>
    public Task OnRetry(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, int retryCount, Context context, ILoggingManager logger)
    {
        if (logger is null)
        {
            return Task.CompletedTask;
        }

        if (result.Result is not null)
        {
            logger.LogWarning($"Request failed with {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
            return Task.CompletedTask;
        }

        logger.LogWarning($"Request failed because network failure. Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
        return Task.CompletedTask;
    }
}
