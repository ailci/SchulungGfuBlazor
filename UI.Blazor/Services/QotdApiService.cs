using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using Infrastructure.Services;

namespace UI.Blazor.Services;

public class QotdApiService(ILogger<QotdApiService> logger, IHttpClientFactory httpClientFactory) : IQotdService
{
    public async Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync()
    {
        var client = httpClientFactory.CreateClient("qotdapi");
        return await client.GetFromJsonAsync<QuoteOfTheDayViewModel>("api/qotd");
    }
}