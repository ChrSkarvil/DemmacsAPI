using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface IProductColorRepository
    {
        Task<ProductColor[]> GetAllProductColorsAsync();
        Task<ProductColor> GetProductColorByIdAsync(int id);
        Task<IEnumerable<ProductColor>> GetProductColorsByProductIdAsync(int id);
        Task<IEnumerable<ProductColor>> GetProductColorsByColorIdAsync(int id);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
