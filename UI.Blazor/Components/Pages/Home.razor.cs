using Application.ViewModels.Qotd;
using Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace UI.Blazor.Components.Pages;
public partial class Home
{
    [Inject] public ILogger<Home> Logger { get; set; } = null!;
    [Inject] public QotdDbContext QotdDbContext { get; set; } = null!;
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation($"{nameof(OnInitializedAsync)} aufgerufen...");

        var quotes = await QotdDbContext.Quotes.Include(c => c.Author).AsNoTracking().ToListAsync();
        var randomQuote = quotes.Shuffle().First();

        QotdViewModel = new QuoteOfTheDayViewModel
        {
            Id = randomQuote.Id,
            QuoteText = randomQuote.QuoteText,
            AuthorBirthDate = randomQuote.Author?.BirthDate,
            AuthorName = randomQuote.Author?.Name ?? string.Empty,
            AuthorDescription = randomQuote.Author?.Description ?? string.Empty,
            AuthorPhoto = randomQuote.Author?.Photo,
            AuthorPhotoMimeType = randomQuote.Author?.PhotoMimeType
        };
    }
}
