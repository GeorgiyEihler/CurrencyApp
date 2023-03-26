namespace CurrencyApi.Infrastructure.CBRPolicySettings;

/// <summary>
/// Settings for policy.
/// </summary>
public class CBRRetryPolicySettings
{
    public int RetryCount { get; set; }
    public TimeSpan MedianFirstRetryDelaySeconds { get; set; }
}
