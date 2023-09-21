using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorController : Controller
    {
        private readonly IProductColorRepository _repository;
        private readonly IMapper _mapper;

        public ProductColorController(IProductColorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProductColorModel[]>> GetProductColors()
        {
            try
            {
                var allProductColors = await _repository.GetAllProductColorsAsync();
                return _mapper.Map<ProductColorModel[]>(allProductColors);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchProductColors(int? productId, int? colorId)
        {
            if (productId.HasValue)
            {
                var productColors = await _repository.GetProductColorsByProductIdAsync(productId.Value);
                if (!productColors.Any())
                {
                    return NotFound("no color found for that product.");
                }

                var productColorModels = _mapper.Map<IEnumerable<ProductColorModel>>(productColors);
                return Ok(productColorModels);
            }
            else if (colorId.HasValue)
            {
                var productColors = await _repository.GetProductColorsByColorIdAsync(colorId.Value);
                if (!productColors.Any())
                {
                    return NotFound("no products found for that color.");
                }

                var productColorModels = _mapper.Map<IEnumerable<ProductColorModel>>(productColors);
                return Ok(productColorModels);
            }
            else
            {
                return BadRequest("Please enter either productId or colorId.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductColorModelCreate>> PostStockProduct(ProductColorModelCreate model)
        {
            try
            {
                //Create a new ProductColor
                var productColor = _mapper.Map<ProductColor>(model);
                _repository.Add(productColor);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/ProductColor/{productColor.ProductColorId}", _mapper.Map<ProductColorModelCreate>(productColor));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        //Laves om til at tage imod productid & colorid i stedet for productcolorid??
        public async Task<ActionResult<ProductColorModelCreate>> PutStockProduct(int id, ProductColorModelCreate model)
        {
            try
            {
                var oldProductColor = await _repository.GetProductColorByIdAsync(id);
                if (oldProductColor == null) return NotFound($"Could not find ProductColor {id}");

                _mapper.Map(model, oldProductColor);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<ProductColorModelCreate>(oldProductColor);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockProduct(int id)
        {
            try
            {
                var oldProductColor = await _repository.GetProductColorByIdAsync(id);
                if (oldProductColor == null) return NotFound();

                _repository.Delete(oldProductColor);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted ProductColor {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The ProductColor");
        }
    }
}
