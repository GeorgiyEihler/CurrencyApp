using Currency.Contract.Context;
using Currency.Model;
using Dapper;

namespace Currency.Repository.CurrencyRepositoy;

public class CerrencyRepository : ICurrencyRepository
{
	private readonly ICurrencyContext _context;
	public CerrencyRepository(ICurrencyContext context)
	{
		_context= context;
	}

	public async Task<IEnumerable<CureencyInformation>> GetCurrencyInformaionAsync()
	{
		var query = """SELECT * FROM "CurrencyInformation" """;

		using var connection = _context.CreateConnection();

		var currencyInformation = await connection.QueryAsync<CureencyInformation>(query);

		return currencyInformation;
    }

	public async Task InsertCurrencyInformationAsync(IEnumerable<CureencyInformation> cureencyInformations)
	{
        var command = """INSERT INTO "CurrencyInformation" ("Id", "CurrencyCBRId", "CurrencyName", "CurrencyEngName", "Nominal", "ParentCode")""" +
           """VALUES (@Id, @CurrencyCBRId, @CurrencyName, @CurrencyEngName, @Nominal, @ParentCode)""";


        using var connection = _context.CreateConnection();

        foreach (var currency in cureencyInformations)
        {
            await connection.ExecuteAsync(command, new
            {
                Id = currency.Id,
                CurrencyCBRId = currency.CurrencyCBRId,
                CurrencyName = currency.CurrencyName,
                CurrencyEngName = currency.CurrencyEngName,
                Nominal = currency.Nominal,
                ParentCode = currency.ParentCode
            });
        }
    }
}
