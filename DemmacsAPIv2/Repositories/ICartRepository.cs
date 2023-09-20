using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface ICartRepository
    {
        Task<Cart[]> GetAllCartsAsync();
        Task<Cart> GetCartByIdAsync(int id);
        Task<IEnumerable<Cart>> GetCartsByCustomerIdAsync(int id);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
