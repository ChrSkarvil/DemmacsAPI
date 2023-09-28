using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public ManufacturerRepository(DemmacsdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Manufacturer[]> GetAllManufacturersAsync()
        {
            var query = _context.Manufacturers;

            return await query.ToArrayAsync();
        }

        public async Task<Manufacturer> GetManufacturerAsync(int id)
        {
            IQueryable<Manufacturer> query = _context.Manufacturers;
            // Query It
            query = query
                .Where(m => m.ManufacturerId == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
