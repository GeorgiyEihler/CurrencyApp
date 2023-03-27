using Currency.Model;

namespace Currency.Contract.Context;

public interface ICurrencyRepository
{
    Task<IEnumerable<CureencyInformation>> GetCurrencyInformaionAsync();

    Task InsertCurrencyInformationAsync(IEnumerable<CureencyInformation> cureencyInformations);
}
