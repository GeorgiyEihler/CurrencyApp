using System.Xml.Serialization;

namespace Currency.Shared.CBRResponseDto;

[XmlRoot("Valuta")]
public record CBRCurrencyInfromationResponseDto 
{
    [XmlAttribute("name")]
    public string? Name { get; init; }

    [XmlElement("Item")]
    public CurrencyInformationDto[] CurrencyInformationDto { get; init; } = null!;
}

public record CurrencyInformationDto
{
    [XmlAttribute("ID")]
    public string? Id { get; init; }

    [XmlElement("Name")]
    public string? Name { get; init; }

    [XmlElement("EngName")]
    public string? EngName { get; init; }

    [XmlElement("Nominal")]
    public int Nominal { get; init; }

    [XmlElement("ParentCode")]
    public string? ParentCode { get; init; }
}