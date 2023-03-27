using Currency.Contract.Context;
using Currency.Contract.CurrencyService;
using Currency.Contract.LogManager;
using Currency.Model;
using Currency.Service.CBRExtentions;
using Currency.Service.CurrencyEndpoints;
using Currency.Shared.CBRResponseDto;

namespace Currency.Service.CBRCurrencyService;

public class CBRService : ICurrencyService
{
    private readonly IHttpClientFactory _cbrClientFacoty;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ILoggingManager _logger;
    public CBRService(ILoggingManager logger, IHttpClientFactory clientFactory, ICurrencyRepository currencyRepository)
    {
        _logger = logger;
        _currencyRepository = currencyRepository;
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

    public async Task InsertCurrencyInformation()
    {
        var currencyInformationDto = await GetCurrencyInformation(true);

        if (currencyInformationDto.CurrencyInformationDto is null || currencyInformationDto.CurrencyInformationDto.Length == 0)
        {
            return;
        }

        var currencyInformation = currencyInformationDto.CurrencyInformationDto.Select(dto =>
            new CureencyInformation
            {
                CurrencyCBRId = dto.Id,
                CurrencyEngName = dto.EngName,
                CurrencyName = dto.Name,
                Id = Guid.NewGuid(),
                Nominal = dto.Nominal,
                ParentCode = dto.ParentCode
            });

        await _currencyRepository.InsertCurrencyInformationAsync(currencyInformation);
    }

    public async Task<IEnumerable<CureencyInformation>> GetCureencyInformationAsync()
    {
        var information = await _currencyRepository.GetCurrencyInformaionAsync();

        return information;
    }
}
