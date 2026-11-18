using System;
using System.Collections.Generic;
using System.Text;
using Application.ViewModels.Author;

namespace Application.Contracts.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorViewModel>> GetAuthorsAsync();
}