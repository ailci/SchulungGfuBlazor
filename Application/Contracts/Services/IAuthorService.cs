using Application.ViewModels.Author;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorViewModel>> GetAuthorsAsync();
    Task<AuthorViewModel> AddAuthorAsync(AuthorForCreateViewModel authorForCreateViewModel);
    Task<bool> DeleteAuthorAsync(Guid authorId);
}