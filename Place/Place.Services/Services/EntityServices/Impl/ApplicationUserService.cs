using Place.Core.Data.Entites;
using Place.Core.Repositories;
using Place.Core.Repositories.Abstract;
using Place.Services.Services.Abstract;

namespace Place.Services.Services.EntityServices.Impl
{
    public class ApplicationUserService : EntityService<IApplicationUserRepository, ApplicationUser>,
        IApplicationUserService
    {
        public ApplicationUserService(IApplicationUserRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public ApplicationUser GetUser(string id)
        {
            return Repository.GetUser(id);
        }

        public ApplicationUser GetApplicationUser(string username, bool isUpdate)
        {
            return Repository.GetUserByUsername(username);
        }
    }
}
