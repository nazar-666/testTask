using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Place.Core.Repositories.Factory
{
    public interface IRepositoryManager
    {
        IApplicationUserRepository ApplicationUsers { get; }
    }
}
