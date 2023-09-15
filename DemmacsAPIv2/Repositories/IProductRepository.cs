using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface IProductRepository
    {
        Task<Product[]> GetAllProductsAsync();
        Task<Product> GetProductAsync(int id);

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        //void Insert(Product product);

        void Update(Product product);

        //void Delete(int productId);

        Task<bool> SaveChangesAsync();
    }
}
