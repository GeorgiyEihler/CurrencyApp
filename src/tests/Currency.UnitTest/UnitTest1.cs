using Polly.Contrib.WaitAndRetry;

namespace Currency.UnitTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var xx = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 2)
            .ToArray();

        Assert.Equal(2, xx.Length);
    }
}