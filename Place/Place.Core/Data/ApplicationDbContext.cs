using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Place.Core.Data.Entites;

namespace Place.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {

        #region DbSet
        public virtual DbSet<Customer> Customers { get; set; }
        #endregion

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
