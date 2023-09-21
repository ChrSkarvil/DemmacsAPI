using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository _repository;
        private readonly IMapper _mapper;

        public OrderItemController(IOrderItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OrderItemModel[]>> GetOrderItems()
        {
            try
            {
                var allOrderItems = await _repository.GetAllOrderItemsAsync();
                return _mapper.Map<OrderItemModel[]>(allOrderItems);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchOrderItem(int? orderItemId, int? productId)
        {
            if (orderItemId.HasValue)
            {
                var orderItem = await _repository.GetOrderItemByIdAsync(orderItemId.Value);
                if (orderItem == null)
                {
                    return NotFound("OrderItem not found.");
                }

                var orderModel = _mapper.Map<OrderItemModel>(orderItem);
                return Ok(orderModel);
            }
            else if (productId.HasValue)
            {
                var orderItems = await _repository.GetOrderItemsByProductIdAsync(productId.Value);
                if (!orderItems.Any())
                {
                    return NotFound("No orderitems found for that product.");
                }

                var orderModels = _mapper.Map<IEnumerable<OrderItemModel>>(orderItems);
                return Ok(orderModels);
            }
            else
            {
                return BadRequest("Please enter either orderItemId or productId.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemModelCreate>> PostOrderItem(OrderItemModelCreate model)
        {
            try
            {
                //Create a new OrderItem
                var orderitem = _mapper.Map<Orderitem>(model);
                _repository.Add(orderitem);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/OrderItem/{orderitem.OrderItemId}", _mapper.Map<OrderItemModelCreate>(orderitem));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItemModelCreate>> PutOrderItem(int id, OrderItemModelCreate model)
        {
            try
            {
                var oldOrderItem = await _repository.GetOrderItemByIdAsync(id);
                if (oldOrderItem == null) return NotFound($"Could not find OrderItem {id}");

                _mapper.Map(model, oldOrderItem);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<OrderItemModelCreate>(oldOrderItem);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            try
            {
                var oldOrderItem = await _repository.GetOrderItemByIdAsync(id);
                if (oldOrderItem == null) return NotFound();

                _repository.Delete(oldOrderItem);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted OrderItem {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The OrderItem");
        }
    }
}
