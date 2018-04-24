using System.Linq;
using Place.Core.Data.Entites;
using Place.Core.Models;
using Place.Core.Repositories.Abstract;

namespace Place.Core.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        IQueryable<CustomerViewModel> GetCustomers();
    }
}
