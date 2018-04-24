using System.Linq;
using Place.Core.Data;
using Place.Core.Data.Entites;
using Place.Core.Models;
using Place.Core.Repositories.Abstract;

namespace Place.Core.Repositories.Impl
{
    class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext сontext) : base(сontext)
        {
        }

        public IQueryable<CustomerViewModel> GetCustomers()
        {
            return DbSet.Select(x => new CustomerViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                CountOfTasks = x.CountOfTasks,
                DurationHours = x.DurationHours,
                ExecutedTasks = x.ExecutedTasks,
                PercentOfExecutedTasks = x.CountOfTasks != 0 ? x.ExecutedTasks * 100.0 / x.CountOfTasks : 0,
                DurationOfExecutedTasks = x.ExecutedTasks != 0 ? x.DurationHours * 1.0 / x.ExecutedTasks : 0
            }).OrderBy(x => x.Id);
        }
    }
}
