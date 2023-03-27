namespace Currency.Model;

public class CureencyInformation
{
    public Guid Id { get; set; }
    public string CurrencyCBRId { get; set; } = null!;
    public string CurrencyName { get; set; } = null!;
    public string? CurrencyEngName { get; set; }
    public int Nominal { get; set; }
    public string? ParentCode { get; set; }
}
