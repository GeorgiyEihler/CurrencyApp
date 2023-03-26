using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CurrencyApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
	private readonly IHttpClientFactory _clientFactory;
	public CurrencyController(IHttpClientFactory clientFactory)
	{
        _clientFactory = clientFactory;

    }

    [HttpGet(Name = "GetCurrencyInformation")]
    public async Task<IActionResult> Get()
	{
        return Ok();
	}

}
