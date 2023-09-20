using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment[]> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByCustomerIdAsync(int id);

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
