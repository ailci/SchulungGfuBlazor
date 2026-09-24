using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Domain.Entities;

public class Author
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public DateOnly? BirthDate { get; set; }

    public ICollection<Quote>? Quotes { get; set; }

    public byte[]? Photo { get; set; }

    public string? PhotoMimeType { get; set; }
}
