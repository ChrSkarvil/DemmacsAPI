using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CartModel[]>> GetCarts()
        {
            try
            {
                var allCarts = await _repository.GetAllCartsAsync();
                return _mapper.Map<CartModel[]>(allCarts);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchCart(int? cartId, int? customerId)
        {
            if (cartId.HasValue)
            {
                var cart = await _repository.GetCartByIdAsync(cartId.Value);
                if (cart == null)
                {
                    return NotFound("Cart not found.");
                }

                var cartModel = _mapper.Map<CartModel>(cart);
                return Ok(cartModel);
            }
            else if (customerId.HasValue)
            {
                var carts = await _repository.GetCartsByCustomerIdAsync(customerId.Value);
                if (!carts.Any())
                {
                    return NotFound("No carts found for the customer.");
                }

                var cartModels = _mapper.Map<IEnumerable<CartModel>>(carts);
                return Ok(cartModels);
            }
            else
            {
                return BadRequest("Please enter either CartId or CustomerId.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CartModelCreate>> PostCart(CartModelCreate model)
        {
            try
            {
                //Create a new Cart
                var cart = _mapper.Map<Cart>(model);
                _repository.Add(cart);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/Cart/{cart.CartId}", _mapper.Map<CartModelCreate>(cart));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CartModelCreate>> PutCart(int id, CartModelCreate model)
        {
            try
            {
                var oldCart = await _repository.GetCartByIdAsync(id);
                if (oldCart == null) return NotFound($"Could not find cart {id}");

                _mapper.Map(model, oldCart);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CartModelCreate>(oldCart);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            try
            {
                var oldCart = await _repository.GetCartByIdAsync(id);
                if (oldCart == null) return NotFound();

                _repository.Delete(oldCart);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted Cart {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The Cart");
        }
    }
}
