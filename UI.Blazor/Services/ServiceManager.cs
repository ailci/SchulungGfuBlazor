using Application.Contracts.Services;

namespace UI.Blazor.Services;

public class ServiceManager(IQotdService qotdService, IAuthorService authorService) : IServiceManager
{
    public IQotdService QotdService => qotdService;
    public IAuthorService AuthorService => authorService;
}