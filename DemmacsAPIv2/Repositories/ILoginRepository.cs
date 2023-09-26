using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;

namespace DemmacsAPIv2.Repositories
{
    public interface ILoginRepository
    {
        Task<IEnumerable<LoginModel>> GetAllLoginsAsync();
        Task<LoginModel> GetLoginAsync(string email);
        Task<Login> FindLogin(string email);

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}
