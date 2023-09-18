using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(DemmacsdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var query = _context.Products
               .Include(product => product.Category)
               .Include(product => product.Manufacture);

            return await query.ToArrayAsync();

        }

        public async Task<Product> GetProductAsync(int id)
        {
            IQueryable<Product> query = _context.Products;
            // Query It
            query = query
                .Include(product => product.Category)
                .Include(product => product.Manufacture)
                .Where(p => p.ProductId == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
