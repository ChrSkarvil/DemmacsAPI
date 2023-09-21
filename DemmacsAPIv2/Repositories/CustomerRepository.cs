using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(DemmacsdbContext context, IMapper mapper)
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

        public async Task<Customer[]> GetAllCustomersAsync()
        {
            var query = _context.Customers
               .Include(c => c.Country)
               .Include(p => p.PostalCodeNavigation);

            return await query.ToArrayAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            IQueryable<Customer> query = _context.Customers;
            // Query It
            query = query
                .Include(c => c.Country)
                .Include(p => p.PostalCodeNavigation)
                .Where(c => c.CustomerId == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
