using Application.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Application.ViewModels.Qotd;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class QotdService(ILogger<QotdService> logger, IDbContextFactory<QotdDbContext> contextFactory, IMapper mapper) : IQotdService
{
    public async Task<QuoteOfTheDayViewModel> GetQuoteOfTheDayAsync()
    {
        logger.LogInformation($"{nameof(GetQuoteOfTheDayAsync)} aufgerufen...");

        await using var context = await contextFactory.CreateDbContextAsync();

        var quotes = await context.Quotes.Include(c => c.Author).ToListAsync();
        var randomQuote = quotes.Shuffle().First();

        //Klassik Manuelles Mapping
        //return new QuoteOfTheDayViewModel
        //{
        //    Id = randomQuote.Id,
        //    QuoteText = randomQuote.QuoteText,
        //    AuthorName = randomQuote.Author?.Name ?? string.Empty,
        //    AuthorDescription = randomQuote.Author?.Description ?? string.Empty,
        //    AuthorBirthDate = randomQuote.Author?.BirthDate,
        //    AuthorPhoto = randomQuote.Author?.Photo,
        //    AuthorPhotoMimeType = randomQuote.Author?.PhotoMimeType
        //};

        //Automapper
        return mapper.Map<QuoteOfTheDayViewModel>(randomQuote);
    }
}