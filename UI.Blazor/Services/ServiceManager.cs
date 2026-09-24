using Application.Contracts.Services;

namespace UI.Blazor.Services;

public class ServiceManager(IQotdService qotdService) : IServiceManager
{
    public IQotdService QotdService => qotdService;
}