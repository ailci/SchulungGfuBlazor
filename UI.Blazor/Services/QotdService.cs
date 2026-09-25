using Application.Contracts.Services;
using Application.ViewModels.Qotd;
using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace UI.Blazor.Services;

public class QotdService(ILogger<QotdService> logger, IDbContextFactory<QotdDbContext> contextFactory, IMapper mapper) : IQotdService
{
    public async Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync()
    {
        logger.LogInformation($"{nameof(GetQuoteOfTheDayAsync)} aufgerufen...");

        await using var context = await contextFactory.CreateDbContextAsync();

        var quotes = await context.Quotes
            .Include(c => c.Author)
            .AsNoTracking()
            .ToListAsync();
        var randomQuote = quotes.Shuffle().First();

        //Klassiker Manuelles Mapping
        //return new QuoteOfTheDayViewModel
        //{
        //    Id = randomQuote.Id,
        //    QuoteText = randomQuote.QuoteText,
        //    AuthorBirthDate = randomQuote.Author?.BirthDate,
        //    AuthorName = randomQuote.Author?.Name ?? string.Empty,
        //    AuthorDescription = randomQuote.Author?.Description ?? string.Empty,
        //    AuthorPhoto = randomQuote.Author?.Photo,
        //    AuthorPhotoMimeType = randomQuote.Author?.PhotoMimeType
        //};

        //Automapper
        return mapper.Map<QuoteOfTheDayViewModel>(randomQuote);
    }
}