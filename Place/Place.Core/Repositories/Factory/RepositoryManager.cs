using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Core.Data;
using Place.Core.Repositories.Factory;
using Place.Core.Repositories.Impl;

namespace Place.Core.Repositories.Factory
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _context;

        #region Private Repository fields

        private IApplicationUserRepository _applicationUserRepository;

        #endregion

        public RepositoryManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IApplicationUserRepository ApplicationUsers => _applicationUserRepository ??
                                                             (_applicationUserRepository = new ApplicationUserRepository(_context));
    }
}
