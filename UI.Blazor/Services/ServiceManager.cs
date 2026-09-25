using Application.Contracts.Services;

namespace UI.Blazor.Services;

public class ServiceManager(
    IQotdService qotdService, 
    IAuthorService authorService, 
    [FromKeyedServices(key:"qotdapi")] IQotdService qotdApiService) : IServiceManager
{
    public IQotdService QotdService => qotdService;
    public IAuthorService AuthorService => authorService;
    public IQotdService QotdApiService => qotdApiService;
}