using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface IOrderItemRepository
    {
        Task<Orderitem[]> GetAllOrderItemsAsync();
        Task<Orderitem> GetOrderItemByIdAsync(int id);
        Task<IEnumerable<Orderitem>> GetOrderItemsByProductIdAsync(int id);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
