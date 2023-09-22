using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface IDeliveryRepository
    {
        Task<Delivery[]> GetAllDeliveriesAsync();
        Task<Delivery> GetDeliveryByIdAsync(int id);

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
