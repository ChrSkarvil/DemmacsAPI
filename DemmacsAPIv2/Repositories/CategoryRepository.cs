using AutoMapper;
using DemmacsAPIv2.Data;
using DemmacsAPIv2.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemmacsAPIv2.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DemmacsdbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DemmacsdbContext context, IMapper mapper)
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

        public async Task<Category[]> GetAllCategoriesAsync()
        {
            var query = _context.Categories;

            return await query.ToArrayAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            IQueryable<Category> query = _context.Categories;
            // Query It
            query = query
                .Where(c => c.CategoryId == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
