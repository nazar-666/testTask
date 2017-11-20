using Place.Core.Data.Entites;
using Place.Core.Repositories.Abstract;

namespace Place.Core.Repositories
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
        ApplicationUser GetUser(string id);

        ApplicationUser GetUserByUsername(string username);
    }
}
