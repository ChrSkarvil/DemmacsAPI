using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int productId);

        void Insert(Product product);

        void Update(Product product);

        void Delete(int productId);

        void Save();
    }
}
