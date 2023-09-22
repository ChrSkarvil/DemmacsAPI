using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public DeliveryRepository(DemmacsdbContext context, IMapper mapper)
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

        public async Task<Delivery[]> GetAllDeliveriesAsync()
        {
            var query = _context.Deliveries
                .Include(p => p.PostalCodeNavigation)
                .Include(c => c.Country);

            return await query.ToArrayAsync();
        }

        public async Task<Delivery> GetDeliveryByIdAsync(int id)
        {
            IQueryable<Delivery> query = _context.Deliveries;
            // Query It
            query = query
                .Include(p => p.PostalCodeNavigation)
                .Include(c => c.Country)
                .Where(p => p.DeliveryId == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
