using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;

namespace Application.ViewModels.Author;

public class AuthorForCreateViewModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public DateOnly? BirthDate { get; set; }

    public IBrowserFile? Photo { get; set; }
}