namespace CurrencyApi.Infrastructure.CBRPolicySettings;

/// <summary>
/// Settings for policy.
/// </summary>
public class CBRRetryPolicySettings
{
    public int RetryCount { get; set; }
    public int MedianFirstRetryDelaySeconds { get; set; }
    public TimeSpan MedianFirstRetryDelay => TimeSpan.FromSeconds(MedianFirstRetryDelaySeconds);
}
