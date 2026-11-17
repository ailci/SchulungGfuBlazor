using Application.ViewModels.Qotd;
using Infrastructure;
using Microsoft.AspNetCore.Components;

namespace UI.Blazor.Components.Pages;
public partial class Home
{
    public QuoteOfTheDayViewModel? QotdViewModel { get; set; }

    [Inject]
    public QotdDbContext QotdDbContext { get; set; }

    protected override void OnInitialized()
    {
        var authors = QotdDbContext.Authors.ToList();

        QotdViewModel = new QuoteOfTheDayViewModel
        {
            AuthorName = "Ich",
            AuthorDescription = "Dozent",
            AuthorBirthDate = new DateOnly(1978,07,13),
            QuoteText = "Lierum Larum Löffelstiel"
        };
    }
}
