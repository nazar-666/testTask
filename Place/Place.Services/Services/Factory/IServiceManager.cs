using Place.Services.Services.EntityServices;

namespace Place.Services.Services.Factory
{
    public interface IServiceManager
    {
        IApplicationUserService ApplicationUserService { get; }
    }
}
