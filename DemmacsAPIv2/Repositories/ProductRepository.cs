using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DemmacsdbContext _context;

        public ProductRepository(DemmacsdbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Product[]> GetAllProductsAsync()
        {
            var query = _context.Products;
            //husk at inkluder category

            return await query.ToArrayAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            IQueryable<Product> query = _context.Products;
            // Query It
            query = query
                .Where(p => p.ProductId == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update(Product product)
        {
            var productDb = _context.Products.Find(product.ProductId);

            if (productDb != null)
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

        //public void Delete(int productId)
        //{
        //    var product = _context.Products.Find(productId);
        //    if (product != null)
        //    {
        //        _context.Remove(product);
        //    }
        //}

        //public IEnumerable<Product> GetAllProducts()
        //{
        //    var allProducts = _context.Products;
        //    return allProducts;
        //}

        //public Product GetById(int productId)
        //{
        //    var product = _context.Products.Find(productId);

        //    return product;
        //}

        //public void Insert(Product product)
        //{
        //    _context.Products.Add(product);
        //}

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

    }
}
