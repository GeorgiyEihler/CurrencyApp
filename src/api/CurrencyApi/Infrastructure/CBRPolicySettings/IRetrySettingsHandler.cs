using Currency.Contract.LogManager;
using Polly;

namespace CurrencyApi.Infrastructure.CBRPolicySettings;

public interface IRetrySettingsHandler
{
    int RetryCount { get; }
    Task OnRetry(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, int retryCount, Context context, ILoggingManager logger);
    TimeSpan SleepDurationProvider(int retryCount);
}
