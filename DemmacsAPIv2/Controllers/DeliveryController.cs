using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryRepository _repository;
        private readonly IMapper _mapper;

        public DeliveryController(IDeliveryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DeliveryModel[]>> GetDeliveries()
        {
            try
            {
                var allDeliveries = await _repository.GetAllDeliveriesAsync();
                return _mapper.Map<DeliveryModel[]>(allDeliveries);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryModel>> GetDelivery(int id)
        {
            try
            {

                var result = await _repository.GetDeliveryByIdAsync(id);

                if (result == null) return NotFound($"Could not find delivery {id}");

                return _mapper.Map<DeliveryModel>(result);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DeliveryModelCreate>> PostDelivery(DeliveryModelCreate model)
        {
            try
            {
                //Create a new Product
                var delivery = _mapper.Map<Delivery>(model);
                _repository.Add(delivery);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/Delivery/{delivery.DeliveryId}", _mapper.Map<DeliveryModelCreate>(delivery));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DeliveryModelCreate>> PutDelivery(int id, DeliveryModelCreate model)
        {
            try
            {
                var oldDelivery = await _repository.GetDeliveryByIdAsync(id);
                if (oldDelivery == null) return NotFound($"Could not find delivery {id}");

                _mapper.Map(model, oldDelivery);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<DeliveryModelCreate>(oldDelivery);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            try
            {
                var oldDelivery = await _repository.GetDeliveryByIdAsync(id);
                if (oldDelivery == null) return NotFound();

                _repository.Delete(oldDelivery);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted Delivery {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The Delivery");
        }
    }
}
