using Currency.Contract.CurrencyService;
using Currency.Contract.LogManager;
using Currency.Service.CBRExtentions;
using Currency.Service.CurrencyEndpoints;
using Currency.Shared.CBRResponseDto;

namespace Currency.Service.CBRCurrencyService;

public class CBRService : ICurrencyService
{
    private readonly IHttpClientFactory _cbrClientFacoty;
    private readonly ILoggingManager _logger;
    public CBRService(ILoggingManager logger, IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _cbrClientFacoty = clientFactory;
    }
    public async Task<CBRCurrencyInfromationResponseDto> GetCurrencyInformation(bool isDaily)
    {
        var client = _cbrClientFacoty.CreateClient("CBRClient");

        var requestUri = CBREndpoints.CurrencyInformation(isDaily);

        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        var responseDto = await response.GetResponseDto<CBRCurrencyInfromationResponseDto>();

        return responseDto;
    }

    public async Task<CBRCurrencyResponseDto> GetCurrencyReate(DateOnly date)
    {
        var client = _cbrClientFacoty.CreateClient("CBRClient");

        var requestUri = CBREndpoints.CurrencyRate(date);

        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        var responseDto = await response.GetResponseDto<CBRCurrencyResponseDto>();

        return responseDto;
    }

    public async Task<CBRCurrencyLastedResponseDto> GetLastedRate()
    {
        var client = _cbrClientFacoty.CreateClient("CBRDailyClient");

        var requestUri = CBREndpoints.Lasted;

        var response = await client.GetAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        var responseDto = await response.GetJsonResponseDto<CBRCurrencyLastedResponseDto>();

        return responseDto;
    }
}
