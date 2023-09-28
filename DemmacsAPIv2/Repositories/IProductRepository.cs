using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;

namespace DemmacsAPIv2.Repositories
{
    public interface IProductRepository
    {
        Task<Product[]> GetAllProductsAsync();
        Task<Product> GetProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int id);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
