namespace Place.Core.Repositories.Factory
{
    public interface IRepositoryManager
    {
        IApplicationUserRepository ApplicationUsers { get; }

        ICustomerRepository Customers { get; }
    }
}
