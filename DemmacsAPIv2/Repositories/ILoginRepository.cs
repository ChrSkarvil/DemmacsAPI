using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;

namespace DemmacsAPIv2.Repositories
{
    public interface ILoginRepository
    {
        Task<IEnumerable<LoginModel>> GetAllLoginsAsync();
        Task<LoginModel> GetLoginAsync(int id);
        Task<Login> FindLogin(int id);

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
