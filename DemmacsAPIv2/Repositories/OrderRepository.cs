using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(DemmacsdbContext context, IMapper mapper)
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

        public async Task<Order[]> GetAllOrdersAsync()
        {
            var query = _context.Orders
                .Include(o => o.Orderitems)
                    .ThenInclude(pr => pr.Product)
                .Include(p => p.Payment)
                .Include(c => c.Customer)
                .Include(d => d.Delivery)
                .Include(dc => dc.Delivery.Country)
                .Include(p => p.Delivery.PostalCodeNavigation);

            return await query.ToArrayAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            IQueryable<Order> query = _context.Orders;
            // Query It
            query = query
                .Include(o => o.Orderitems)
                    .ThenInclude(pr => pr.Product)
                .Include(p => p.Payment)
                .Include(c => c.Customer)
                .Include(d => d.Delivery)
                .Include(dc => dc.Delivery.Country)
                .Include(p => p.Delivery.PostalCodeNavigation)
                .Where(o => o.OrderId == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int id)
        {
            IQueryable<Order> query = _context.Orders;
            // Query It
            query = query
                .Include(o => o.Orderitems)
                    .ThenInclude(pr => pr.Product)
                .Include(p => p.Payment)
                .Include(c => c.Customer)
                .Include(d => d.Delivery)
                .Include(dc => dc.Delivery.Country)
                .Include(p => p.Delivery.PostalCodeNavigation)
                .Where(o => o.CustomerId == id);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDeliveryIdAsync(int id)
        {
            IQueryable<Order> query = _context.Orders;
            // Query It
            query = query
                .Include(o => o.Orderitems)
                    .ThenInclude(pr => pr.Product)
                .Include(p => p.Payment)
                .Include(c => c.Customer)
                .Include(d => d.Delivery)
                .Include(dc => dc.Delivery.Country)
                .Include(p => p.Delivery.PostalCodeNavigation)
                .Where(o => o.DeliveryId == id);
            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
