using Currency.Contract.Context;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Currency.Repository.DapperContext;

public class CurrencyDbContext : ICurrencyContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public CurrencyDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("CurrencyConnectionString");
    }

    public IDbConnection CreateConnection() =>
        new NpgsqlConnection(_connectionString);
}
