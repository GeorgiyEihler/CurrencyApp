using System.Data;

namespace Currency.Contract.Context;

public interface ICurrencyContext
{
    IDbConnection CreateConnection();
}
