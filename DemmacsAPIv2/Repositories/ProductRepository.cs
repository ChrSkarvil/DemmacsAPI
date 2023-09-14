using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DemmacsdbContext _context;

        public ProductRepository(DemmacsdbContext context)
        {
            _context = context;
        }

        public void Delete(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Remove(product);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            var allProducts = _context.Products;
            return allProducts;
        }

        public Product GetById(int productId)
        {
            var product = _context.Products.Find(productId);

            return product;
        }

        public void Insert(Product product)
        {
            _context.Products.Add(product);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            var productDb = _context.Products.Find(product.ProductId);

            if (productDb != null )
            {
                product.ProductName = product.ProductName;
                product.ProductPrice = product.ProductPrice;
                product.Description = product.Description;
                product.Dimensions = product.Dimensions;
                product.Weight = product.Weight;
                product.CategoryId = product.CategoryId;
                product.ManufactureId = product.ManufactureId;
                product.Image = product.Image;
            }
        }
    }
}
