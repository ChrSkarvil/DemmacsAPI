using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public CartRepository(DemmacsdbContext context, IMapper mapper)
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

        public async Task<Cart[]> GetAllCartsAsync()
        {
            var query = _context.Carts
                .Include(c => c.Customer)
                .Include(p => p.Product);

            return await query.ToArrayAsync();
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            IQueryable<Cart> query = _context.Carts;
            // Query It
            query = query
                .Include(c => c.Customer)
                .Include(p => p.Product)
                .Where(c => c.CartId == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Cart>> GetCartsByCustomerIdAsync(int id)
        {
            IQueryable<Cart> query = _context.Carts;
            // Query It
            query = query
                .Include(c => c.Customer)
                .Include(p => p.Product)
                .Where(p => p.CustomerId == id);
            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
