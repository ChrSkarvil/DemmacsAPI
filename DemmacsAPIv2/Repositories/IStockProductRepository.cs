using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface IStockProductRepository
    {
        Task<StockProduct[]> GetAllStockProductsAsync();
        Task<StockProduct> GetStockProductByIdAsync(int id);
        Task<IEnumerable<StockProduct>> GetStockProductsByStockIdAsync(int id);
        Task<IEnumerable<StockProduct>> GetStockProductsByProductIdAsync(int id);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
