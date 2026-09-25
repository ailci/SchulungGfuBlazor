using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services;

public class QotdDbService(ILogger<QotdDbService> logger, IDbContextFactory<QotdDbContext> contextFactory, IMapper mapper) : IQotdService
{
    public async Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync()
    {
        logger.LogInformation($"{nameof(GetQuoteOfTheDayAsync)} API aufgerufen...");
        await using var context = await contextFactory.CreateDbContextAsync();

        var quotes = await context.Quotes
            .Include(c => c.Author)
            .AsNoTracking()
            .ToListAsync();

        return mapper.Map<QuoteOfTheDayViewModel>(quotes.Shuffle().First());
    }
}