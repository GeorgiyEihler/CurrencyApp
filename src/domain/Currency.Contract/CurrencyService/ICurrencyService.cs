using Currency.Model;
using Currency.Shared.CBRResponseDto;

namespace Currency.Contract.CurrencyService;

public interface ICurrencyService
{
    Task<CBRCurrencyInfromationResponseDto> GetCurrencyInformation(bool isDaily);

    Task<CBRCurrencyResponseDto> GetCurrencyReate(DateOnly date);

    Task<CBRCurrencyLastedResponseDto> GetLastedRate();

    Task InsertCurrencyInformation();

    Task<IEnumerable<CureencyInformation>> GetCureencyInformationAsync();
}
