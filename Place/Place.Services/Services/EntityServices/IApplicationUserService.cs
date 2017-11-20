using Place.Core.Data.Entites;
using Place.Services.Services.Abstract;

namespace Place.Services.Services.EntityServices
{
    public interface IApplicationUserService : IEntityService<ApplicationUser>
    {
        ApplicationUser GetUser(string id);
        ApplicationUser GetApplicationUser(string username, bool isUpdate);
    }
}
