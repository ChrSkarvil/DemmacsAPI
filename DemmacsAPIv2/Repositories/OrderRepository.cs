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
                .Include(o => o.OrderItem)
                .Include(o => o.OrderItem.Product)
                .Include(p => p.Payment)
                .Include(c => c.Customer)
                .Include(c => c.Delivery)
                .Include(c => c.Delivery.Country)
                .Include(c => c.Delivery.PostalCodeNavigation);

            return await query.ToArrayAsync();
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetOrdersByDeliveryIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
