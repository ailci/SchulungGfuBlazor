using Application.Contracts.Services;
using Application.Utilities;
using Application.ViewModels.Author;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace UI.Blazor.Services;

public class AuthorService(ILogger<QotdService> logger, IDbContextFactory<QotdDbContext> contextFactory, IMapper mapper) : IAuthorService
{
    public async Task<IEnumerable<AuthorViewModel>> GetAuthorsAsync()
    {
        logger.LogInformation($"{nameof(GetAuthorsAsync)} aufgerufen...");

        await using var context = await contextFactory.CreateDbContextAsync();

        return mapper.Map<IEnumerable<AuthorViewModel>>(await context.Authors.AsNoTracking().ToListAsync());
    }

    public async Task<AuthorViewModel> AddAuthorAsync(AuthorForCreateViewModel authorForCreateViewModel)
    {
        logger.LogInformation($"{nameof(AddAuthorAsync)} mit AuthorForCreate {authorForCreateViewModel.LogAsJson()} aufgerufen...");
        await using var context = await contextFactory.CreateDbContextAsync();

        var authorEntity = mapper.Map<Author>(authorForCreateViewModel);

        //Falls Bild ausgewählt
        if (authorForCreateViewModel.Photo is not null)
        {
            (authorEntity.Photo, authorEntity.PhotoMimeType) = await authorForCreateViewModel.Photo.GetFileAsync();
        }

        await context.Authors.AddAsync(authorEntity);
        await context.SaveChangesAsync();

        return mapper.Map<AuthorViewModel>(authorEntity);
    }

    public async Task<bool> DeleteAuthorAsync(Guid authorId)
    {
        logger.LogInformation($"{nameof(DeleteAuthorAsync)} mit AuthorId {authorId} aufgerufen...");
        await using var context = await contextFactory.CreateDbContextAsync();

        //var author = context.Authors.Where(c => c.Id == authorId);
        //var author = await context.Authors.FirstOrDefaultAsync(c => c.Id == authorId);
        //var author = await context.Authors.SingleOrDefaultAsync(c => c.Id == authorId);
        var author = await context.Authors.FindAsync(authorId);
        
        if (author is null) return false;

        //Variante ohne FindAsync
        //var author2 = new Author { Id = authorId, Name = "", Description = "" };

        context.Authors.Remove(author);

        return await context.SaveChangesAsync() > 0;
    }
}