using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<OrderModel[]>> GetOrders()
        {
            try
            {
                var allOrders = await _repository.GetAllOrdersAsync();
                return _mapper.Map<OrderModel[]>(allOrders);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchOrder(int? orderId, int? customerId, int? deliveryId)
        {
            if (orderId.HasValue)
            {
                var order = await _repository.GetOrderByIdAsync(orderId.Value);
                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                var orderModel = _mapper.Map<OrderModel>(order);
                return Ok(orderModel);
            }
            else if (customerId.HasValue)
            {
                var orders = await _repository.GetOrdersByCustomerIdAsync(customerId.Value);
                if (!orders.Any())
                {
                    return NotFound("No orders found for the customer.");
                }

                var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
                return Ok(orderModels);
            }            
            else if (deliveryId.HasValue)
            {
                var orders = await _repository.GetOrdersByDeliveryIdAsync(deliveryId.Value);
                if (!orders.Any())
                {
                    return NotFound("No orders found for the delivery.");
                }

                var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);
                return Ok(orderModels);
            }
            else
            {
                return BadRequest("Please enter either orderId, customerId or deliveryId.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderModelCreate>> PostOrder(OrderModelCreate model)
        {
            try
            {
                //Create a new Order
                var order = _mapper.Map<Order>(model);
                _repository.Add(order);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/Order/{order.OrderId}", _mapper.Map<OrderModelCreate>(order));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }
    }
}
