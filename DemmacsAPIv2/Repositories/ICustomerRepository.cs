using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer[]> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
