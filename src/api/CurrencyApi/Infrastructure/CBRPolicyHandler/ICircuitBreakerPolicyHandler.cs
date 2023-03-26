using Currency.Contract.LogManager;
using Polly;

namespace CurrencyApi.Infrastructure.CBRPolicyHandler;

public interface ICircuitBreakerPolicyHandler
{
    double FailureThreshold { get; }

    TimeSpan SamplingDuration { get; }

    int MinimumThroughput { get; }

    TimeSpan DurationOfBreak { get; }

    void OnBreak(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, ILoggingManager logger);

    void OnReset(ILoggingManager logger);

    void OnHalfOpen(ILoggingManager logger);
}
