using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
	public CurrencyController()
	{

	}

    [HttpGet(Name = "GetCurrencyInformation")]
    public async Task<IActionResult> Get()
	{
		return Ok();
	}

}
