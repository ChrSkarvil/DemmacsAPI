using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class ProductColorRepository : IProductColorRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public ProductColorRepository(DemmacsdbContext context, IMapper mapper)
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

        public async Task<ProductColor[]> GetAllProductColorsAsync()
        {
            var query = _context.ProductColors
            .Include(c => c.Color)
            .Include(p => p.Product);

            return await query.ToArrayAsync();
        }

        public async Task<ProductColor> GetProductColorByIdAsync(int id)
        {
            IQueryable<ProductColor> query = _context.ProductColors;
            // Query It
            query = query
                .Include(c => c.Color)
                .Include(p => p.Product)
                .Where(pc => pc.ProductColorId == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductColor>> GetProductColorsByColorIdAsync(int id)
        {
            IQueryable<ProductColor> query = _context.ProductColors;
            // Query It
            query = query
                .Include(c => c.Color)
                .Include(p => p.Product)
                .Where(c => c.ColorId == id);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<ProductColor>> GetProductColorsByProductIdAsync(int id)
        {
            IQueryable<ProductColor> query = _context.ProductColors;
            // Query It
            query = query
                .Include(c => c.Color)
                .Include(p => p.Product)
                .Where(p => p.ProductId == id);
            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
