using Place.Core.Data;
using Place.Core.Repositories.Abstract;
using Place.Core.Repositories.Factory;
using Place.Services.Services.EntityServices;
using Place.Services.Services.EntityServices.Impl;

namespace Place.Services.Services.Factory
{
    public class ServiceManager : IServiceManager
    {
        private IRepositoryManager _repositoryManager;
        private IUnitOfWork _unitOfWork;

        private IApplicationUserService _applicationUserService;
        private ICustomerService _customerService;

        public ServiceManager()
        {
            var dbContext = ApplicationDbContext.Create();
            Init(new UnitOfWork(dbContext), new RepositoryManager(dbContext));
        }

        public ServiceManager(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            Init(unitOfWork, repositoryManager);
        }

        private void Init(IUnitOfWork unitOfWork, IRepositoryManager repositoryManager)
        {
            _unitOfWork = unitOfWork;
            _repositoryManager = repositoryManager;
        }

        public IApplicationUserService ApplicationUserService => _applicationUserService ??
            (_applicationUserService = new ApplicationUserService(_repositoryManager, _unitOfWork));

        public ICustomerService CustomerService => _customerService ??
            (_customerService = new CustomerService(_repositoryManager, _unitOfWork));
    }
}
