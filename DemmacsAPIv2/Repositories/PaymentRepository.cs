using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public PaymentRepository(DemmacsdbContext context, IMapper mapper)
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

        public async Task<Payment[]> GetAllPaymentsAsync()
        {
            var query = _context.Payments
                .Include(p => p.Customer);
            return await query.ToArrayAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            IQueryable<Payment> query = _context.Payments;
            // Query It
            query = query
                .Include(p => p.Customer)
                .Where(p => p.PaymentId == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Payment>> GetPaymentsByCustomerIdAsync(int id)
        {
            IQueryable<Payment> query = _context.Payments;
            // Query It
            query = query
                .Include(p => p.Customer)
                .Where(p => p.CustomerId == id);
            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
