using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category[]> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
