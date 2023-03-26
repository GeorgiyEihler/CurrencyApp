namespace Currency.Contract.LogManager;

public interface ILoggingManager
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? ex);
    void LogInformation(string message);
    void LogDebug(string message);
}
