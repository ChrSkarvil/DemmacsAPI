using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public OrderItemRepository(DemmacsdbContext context, IMapper mapper)
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

        public async Task<Orderitem[]> GetAllOrderItemsAsync()
        {
            var query = _context.Orderitems
                .Include(p => p.Product);

            return await query.ToArrayAsync();
        }

        public async Task<Orderitem> GetOrderItemByIdAsync(int id)
        {
            IQueryable<Orderitem> query = _context.Orderitems;
            // Query It
            query = query
                .Include(p => p.Product)
                .Where(o => o.OrderItemId == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Orderitem>> GetOrderItemsByProductIdAsync(int id)
        {
            IQueryable<Orderitem> query = _context.Orderitems;
            // Query It
            query = query
                .Include(p => p.Product)
                .Where(o => o.OrderItemId == id);
            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
