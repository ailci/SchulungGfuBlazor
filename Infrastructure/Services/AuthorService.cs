using Application.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Application.ViewModels.Author;
using Domain.Entities;

namespace Infrastructure.Services;

public class AuthorService(ILogger<AuthorService> logger, IDbContextFactory<QotdDbContext> contextFactory) : IAuthorService
{
    public async Task<IEnumerable<AuthorViewModel>> GetAuthorsAsync()
    {
        logger.LogInformation($"{nameof(GetAuthorsAsync)} aufgerufen...");
        await using var context = await contextFactory.CreateDbContextAsync();

        var authors = await context.Authors.AsNoTracking().ToListAsync();

        //Klassik foreach
        //var authorViewModels = new List<AuthorViewModel>();
        //foreach (Author a in authors)
        //{
        //    authorViewModels.Add(new AuthorViewModel
        //    {
        //        Id = a.Id,
        //        Name = a.Name,
        //        Description = a.Description,
        //        BirthDate = a.BirthDate,
        //        Photo = a.Photo,
        //        PhotoMimeType = a.PhotoMimeType
        //    });
        //}

        var authorViewModels = authors.Select(a => new AuthorViewModel
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            BirthDate = a.BirthDate,
            Photo = a.Photo,
            PhotoMimeType = a.PhotoMimeType
        });

        return authorViewModels;
    }
}