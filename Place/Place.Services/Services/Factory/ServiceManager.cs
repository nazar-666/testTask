using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IApplicationUserService ApplicationUserService
        {
            get
            {
                return _applicationUserService ??
                       (_applicationUserService =
                           new ApplicationUserService(_repositoryManager.ApplicationUsers, _unitOfWork));
            }
        }
    }
}
