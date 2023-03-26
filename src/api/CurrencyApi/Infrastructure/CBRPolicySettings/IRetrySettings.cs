using Polly;

namespace CurrencyApi.Infrastructure.CBRPolicySettings;

public interface IRetrySettingsHandler
{
    int RetryCount { get; }
    Task OnRetry(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, int retryCount, Context context);
    TimeSpan SleepDurationProvider(int retryCount);
}
