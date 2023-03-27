using Currency.Contract.CurrencyService;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CurrencyApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;
	public CurrencyController(ICurrencyService currencyService)
	{
        _currencyService= currencyService;
    }

    [HttpGet(Name = "GetCurrencyInformation")]
    public async Task<IActionResult> Get()
	{
        await _currencyService.InsertCurrencyInformation();

        var result = await _currencyService.GetCureencyInformationAsync();

        return Ok(result);
	}

}
