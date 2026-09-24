using Application.Contracts.Services;
using Application.ViewModels.Author;
using AutoMapper;
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
}