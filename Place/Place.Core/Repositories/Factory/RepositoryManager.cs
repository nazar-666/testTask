using Place.Core.Data;
using Place.Core.Repositories.Impl;

namespace Place.Core.Repositories.Factory
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        #region Private Repository fields
        private IApplicationUserRepository _applicationUserRepository;
        private ICustomerRepository _customerRepository;
        #endregion

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IApplicationUserRepository ApplicationUsers => _applicationUserRepository ??
                                                             (_applicationUserRepository = new ApplicationUserRepository(_context));

        public ICustomerRepository Customers => _customerRepository ??
                                                             (_customerRepository = new CustomerRepository(_context));
    }
}
