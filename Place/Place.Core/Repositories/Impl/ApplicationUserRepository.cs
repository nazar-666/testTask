using System.Linq;
using Place.Core.Data;
using Place.Core.Data.Entites;
using Place.Core.Repositories.Abstract;

namespace Place.Core.Repositories.Impl
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext сontext) : base(сontext)
        {
        }

        public ApplicationUser GetUser(string id)
        {
            return DbSet.FirstOrDefault(u => u.Id == id);
        }
        public ApplicationUser GetUserByUsername(string username)
        {
            return DbSet.FirstOrDefault(u => u.UserName == username);
        }
    }
}
