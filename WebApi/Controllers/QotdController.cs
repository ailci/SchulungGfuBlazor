using Application.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]  // localhost:7009/api/qotd
[ApiController]
public class QotdController(IQotdService qotdService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetQuoteOfTheDay()
    {
        return Ok(await qotdService.GetQuoteOfTheDayAsync());
    }
}