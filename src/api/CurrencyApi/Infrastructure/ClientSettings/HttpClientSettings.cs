namespace CurrencyApi.Infrastructure.ClientSettings;

/// <summary>
/// Http client appsettings.
/// </summary>
public class HttpClientSettings
{
    /// <summary>
    /// Http client name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Client base uri.
    /// </summary>
    public string BaseUri { get; set; } = null!;
}