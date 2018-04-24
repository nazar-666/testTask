using System.Linq;
using Place.Core.Data.Entites;
using Place.Core.Models;
using Place.Services.Services.Abstract;

namespace Place.Services.Services.EntityServices
{
    public interface ICustomerService : IEntityService<Customer>
    {
        IQueryable<CustomerViewModel> GetCustomers();
    }
}
