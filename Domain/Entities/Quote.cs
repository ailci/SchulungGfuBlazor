using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Quote
{
    public Guid Id { get; set; }

    public required string QuoteText { get; set; }

    public Author? Author { get; set; }

    public Guid AuthorId { get; set; }
}
