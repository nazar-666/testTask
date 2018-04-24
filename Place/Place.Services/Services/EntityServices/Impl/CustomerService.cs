using System.Linq;
using Place.Core.Data.Entites;
using Place.Core.Models;
using Place.Core.Repositories;
using Place.Core.Repositories.Abstract;
using Place.Core.Repositories.Factory;
using Place.Services.Services.Abstract;

namespace Place.Services.Services.EntityServices.Impl
{
    public class CustomerService : EntityService<ICustomerRepository, Customer>, ICustomerService
    {
        public CustomerService(IRepositoryManager repositoryManager, IUnitOfWork unitOfWork)
            : base(repositoryManager.Customers, unitOfWork)
        {
            
        }

        public IQueryable<CustomerViewModel> GetCustomers()
        {
            return Repository.GetCustomers();
        }
    }
}
