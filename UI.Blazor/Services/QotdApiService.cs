using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace UI.Blazor.Services;

public class QotdApiService(ILogger<QotdApiService> logger, IHttpClientFactory clientFactory, IMapper mapper) : IQotdService
{
    public async Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync()
    {
        logger.LogInformation($"{nameof(GetQuoteOfTheDayAsync)} API aufgerufen...");

        var client = clientFactory.CreateClient("qotdapiservice");
        return await client.GetFromJsonAsync<QuoteOfTheDayViewModel>("api/qotd");
    }
}