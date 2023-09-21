using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockProductController : Controller
    {
        private readonly IStockProductRepository _repository;
        private readonly IMapper _mapper;

        public StockProductController(IStockProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<StockProductModel[]>> GetStockProducts()
        {
            try
            {
                var allStockProducts = await _repository.GetAllStockProductsAsync();
                return _mapper.Map<StockProductModel[]>(allStockProducts);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchStockProducts(int? productId, int? stockId)
        {
            if (productId.HasValue)
            {
                var stockProducts = await _repository.GetStockProductsByProductIdAsync(productId.Value);
                if (!stockProducts.Any())
                {
                    return NotFound("Stock not found for that product.");
                }

                var stockProductModels = _mapper.Map<IEnumerable<StockProductModel>>(stockProducts);
                return Ok(stockProductModels);
            }
            else if (stockId.HasValue)
            {
                var stockProducts = await _repository.GetStockProductsByStockIdAsync(stockId.Value);
                if (!stockProducts.Any())
                {
                    return NotFound("No stock found for the stock.");
                }

                var stockProductModels = _mapper.Map<IEnumerable<StockProductModel>>(stockProducts);
                return Ok(stockProductModels);
            }
            else
            {
                return BadRequest("Please enter either productId or stockId.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<StockProductModelCreate>> PostStockProduct(StockProductModelCreate model)
        {
            try
            {
                //Create a new StockProduct
                var stockProduct = _mapper.Map<StockProduct>(model);
                _repository.Add(stockProduct);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/StockProduct/{stockProduct.StockProductId}", _mapper.Map<StockProductModelCreate>(stockProduct));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        //Laves om til at tage imod stockid & productid i stedet for stockproductid??
        public async Task<ActionResult<StockProductModelCreate>> PutStockProduct(int id, StockProductModelCreate model)
        {
            try
            {
                var oldStockProduct = await _repository.GetStockProductByIdAsync(id);
                if (oldStockProduct == null) return NotFound($"Could not find StockProduct {id}");

                _mapper.Map(model, oldStockProduct);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<StockProductModelCreate>(oldStockProduct);
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
                var oldStockProduct = await _repository.GetStockProductByIdAsync(id);
                if (oldStockProduct == null) return NotFound();

                _repository.Delete(oldStockProduct);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted StockProduct {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The StockProduct");
        }
    }
}
