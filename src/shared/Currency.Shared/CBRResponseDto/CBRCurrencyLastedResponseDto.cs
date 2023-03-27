using System.Globalization;
using System.Text.Json.Serialization;

namespace Currency.Shared.CBRResponseDto;

public record CBRCurrencyLastedResponseDto
{
    [JsonPropertyName("disclaimer")]
    public string Disclaimer { get; init; }

    [JsonPropertyName("date")]
    public string DateString { get; init; }

    [JsonIgnore]
    public DateTime Date => DateTime.ParseExact(DateString.Replace("-", "."), "yyyy.MM.dd", CultureInfo.InvariantCulture);

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; init; }

    [JsonPropertyName("base")]
    public string Base { get; init; }

    [JsonPropertyName("rates")]
    public Dictionary<string, decimal> Rates { get; init; }
}