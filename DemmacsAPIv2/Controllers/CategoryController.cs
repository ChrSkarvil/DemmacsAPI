using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryModel[]>> GetCategories()
        {
            try
            {
                var allCategories = await _repository.GetAllCategoriesAsync();
                return _mapper.Map<CategoryModel[]>(allCategories);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategory(int id)
        {
            try
            {

                var result = await _repository.GetCategoryAsync(id);

                if (result == null) return NotFound($"Could not find Category {id}");

                return _mapper.Map<CategoryModel>(result);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryModel>> PostCategory(CategoryModel model)
        {
            try
            {
                //Create a new Product
                var category = _mapper.Map<Category>(model);
                _repository.Add(category);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/Product/{category.CategoryId}", _mapper.Map<CategoryModel>(category));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryModel>> PutCategory(int id, CategoryModel model)
        {
            try
            {
                var oldCategory = await _repository.GetCategoryAsync(id);
                if (oldCategory == null) return NotFound($"Could not find Category {id}");

                _mapper.Map(model, oldCategory);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CategoryModel>(oldCategory);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var oldCategory = await _repository.GetCategoryAsync(id);
                if (oldCategory == null) return NotFound();

                _repository.Delete(oldCategory);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted Category {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The Category");
        }
    }
}
