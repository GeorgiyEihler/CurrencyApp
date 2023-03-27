using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Currency.Shared.CBRResponseDto;

[XmlRoot("ValCurs")]
public record CBRCurrencyResponseDto
{
    [XmlAttribute("Date")]
    public string DateString { get; init; }

    [XmlIgnore]
    public DateTime Date => 
        DateTime.ParseExact(DateString, "dd.MM.yyyy", CultureInfo.InvariantCulture);

    [XmlAttribute("name")]
    public string Name { get; init; }

    [XmlElement("Valute")]
    public CBRCurrencyRateResponseDto[] CBRCurrencyRateResponseDto { get; init; }
}

public class CBRCurrencyRateResponseDto
{
    [XmlAttribute("ID")]
    public string Id { get; init; }

    [XmlElement("NumCode")]
    public int NumCode { get; init; }

    [XmlElement("CharCode")]
    public string CharCode { get; init; }

    [XmlElement("Nominal")]
    public int Nominal { get; init; }

    [XmlElement("Name")]
    public string Name { get; init; }

    private string _value;

    [XmlElement("Value")]
    public string Value
    {
        get => _value;
        init => _value = value is null ? "0" : value.Replace(',', '.');
    }

    [XmlIgnore]
    public decimal DecimalValue =>
        decimal.Parse(Value, CultureInfo.InvariantCulture);
}
