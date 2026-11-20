using Application.Contracts.Services;
using Application.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]  // => localhost:7083/api/qotd
[ApiController]
public class QotdController(ILogger<QotdController> logger, IServiceManager serviceManager) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetQuoteOfTheDay()
    {
        logger.LogInformation($"{nameof(GetQuoteOfTheDay)} API aufgerufen...");
        var qotdVm = await serviceManager.QotdService.GetQuoteOfTheDayAsync();

        logger.LogInformation($"QotdVm: {qotdVm?.LogAsJson()}");
        
        return Ok(qotdVm);
    }
}