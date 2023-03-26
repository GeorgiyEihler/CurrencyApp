using Currency.Contract.LogManager;
using NLog;

namespace Currency.Logger;

public class CurrencyLogManager : ILoggingManager
{
    private static ILogger logger = LogManager.GetCurrentClassLogger();

    public CurrencyLogManager()
    {

    }
    public void LogDebug(string message) =>
        logger.Debug(message);

    public void LogError(string message, Exception? ex) =>
        logger.Error(ex, message);

    public void LogInfo(string message) =>
        logger.Info(message);

    public void LogInformation(string message) =>
        logger.Info(message);

    public void LogWarning(string message) =>
         logger.Warn(message);
}
