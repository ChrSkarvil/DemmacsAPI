using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class StockProductRepository : IStockProductRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public StockProductRepository(DemmacsdbContext context, IMapper mapper)
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

        public async Task<StockProduct[]> GetAllStockProductsAsync()
        {
            var query = _context.StockProducts
                .Include(s => s.Stock)
                .Include(p => p.Product);

            return await query.ToArrayAsync();
        }

        public async Task<StockProduct> GetStockProductByIdAsync(int id)
        {
            IQueryable<StockProduct> query = _context.StockProducts;
            // Query It
            query = query
                .Include(s => s.Stock)
                .Include(p => p.Product)
                .Where(sp => sp.StockProductId == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StockProduct>> GetStockProductsByProductIdAsync(int id)
        {
            IQueryable<StockProduct> query = _context.StockProducts;
            // Query It
            query = query
                .Include(s => s.Stock)
                .Include(p => p.Product)
                .Where(p => p.ProductId == id);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<StockProduct>> GetStockProductsByStockIdAsync(int id)
        {
            IQueryable<StockProduct> query = _context.StockProducts;
            // Query It
            query = query
                .Include(s => s.Stock)
                .Include(p => p.Product)
                .Where(s => s.StockId == id);
            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
