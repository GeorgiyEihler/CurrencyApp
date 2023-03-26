namespace CurrencyApi.Infrastructure.CBRPolicySettings;

/// <summary>
/// Settings for CircuitBreakerPolicy.
/// </summary>
public class CBRCircuitBreakerPolicySettings
{
    /// <summary>
    /// The proportion of failures at which to break. A double between 0 and 1. 
    /// For example, 0.5 represents break on 50% or more of actions through the circuit resulting in a handled failure.
    /// </summary>
    public double FailureThreshold { get; set; }

    /// <summary>
    /// The failure rate is considered for actions over this period. 
    /// Successes/failures older than the period are discarded from metrics.
    /// </summary>
    public TimeSpan SamplingDuration { get; set; }

    /// <summary>
    /// This many calls must have passed through the circuit within the active 
    /// samplingDuration for the circuit to consider breaking.
    /// </summary>
    public int MinimumThroughput { get; set; }

    /// <summary>
    /// Period has elapsed.
    /// </summary>
    public TimeSpan DurationOfBreak { get; set; }

}
