using Currency.Contract.LogManager;
using CurrencyApi.Infrastructure.CBRPolicySettings;
using Polly;

namespace CurrencyApi.Infrastructure.CBRPolicyHandler;

public class CBRCircuitBreakerPolicyHandler : ICircuitBreakerPolicyHandler
{
    public CBRCircuitBreakerPolicyHandler(CBRCircuitBreakerPolicySettings settigs)
    {
        ArgumentNullException.ThrowIfNull("settigs");

        (FailureThreshold, SamplingDuration, MinimumThroughput, DurationOfBreak) =
            (settigs.FailureThreshold, settigs.SamplingDuration, settigs.MinimumThroughput, settigs.DurationOfBreak);

    }

    public double FailureThreshold { get; }

    public TimeSpan SamplingDuration { get; }

    public int MinimumThroughput { get; }

    public TimeSpan DurationOfBreak { get; }

    public void OnReset(ILoggingManager logger)
    {
        logger.LogWarning($"Circuit breaker reset.");
    }

    public void OnHalfOpen(ILoggingManager logger)
    {
        logger.LogWarning($"Circuit breaker half-open.");
    }

    public void OnBreak(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, ILoggingManager logger)
    {
        if (result.Result is not null)
        {
            logger.LogWarning($"Circuit breaker opened with {result.Result.StatusCode}. Waiting {timeSpan}");
        }

        logger.LogWarning($"RCircuit breaker opened with. Waiting {timeSpan}");
    }
}
