namespace Currency.Service.CurrencyEndpoints;

public class CBREndpoints
{
    public static string Lasted = "latest.js";

    public static string CurrencyRate(DateOnly date) =>
        $"scripts/XML_daily.asp?date_req={date.Day:00}/{date.Month:00}/{date.Year:0000}";

    public static string CurrencyInformation(bool isDaily) => 
        $"scripts/XML_val.asp?d={(isDaily ? "0" : "1")}";
}
