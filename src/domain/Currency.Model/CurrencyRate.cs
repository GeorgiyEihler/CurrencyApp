namespace Currency.Model;

public class CurrencyRate
{
    public Guid Id { get; set; }
    public decimal Rate { get; set; }
    public Guid CurrencyId { get; set; }
    public DateTime RateDate { get; set; }
}
