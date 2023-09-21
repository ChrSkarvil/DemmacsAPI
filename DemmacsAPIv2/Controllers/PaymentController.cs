using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _repository;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentModel[]>> GetPayments()
        {
            try
            {
                var allPayments = await _repository.GetAllPaymentsAsync();
                return _mapper.Map<PaymentModel[]>(allPayments);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpGet("search")]
        public async Task<ActionResult> SearchPayment(int? paymentId, int? customerId)
        {
            if (paymentId.HasValue)
            {
                var payment = await _repository.GetPaymentByIdAsync(paymentId.Value);
                if (payment == null)
                {
                    return NotFound("Payment not found.");
                }

                var paymentModel = _mapper.Map<PaymentModel>(payment);
                return Ok(paymentModel);
            }
            else if (customerId.HasValue)
            {
                var payments = await _repository.GetPaymentsByCustomerIdAsync(customerId.Value);
                if (!payments.Any())
                {
                    return NotFound("No payments found for the customer.");
                }

                var paymentModels = _mapper.Map<IEnumerable<PaymentModel>>(payments);
                return Ok(paymentModels);
            }
            else
            {
                return BadRequest("Please enter either PaymentId or CustomerId.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentModelCreate>> PostPayment(PaymentModelCreate model)
        {
            try
            {
                //Create a new Payment
                var product = _mapper.Map<Payment>(model);
                _repository.Add(product);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/Payment/{product.PaymentId}", _mapper.Map<PaymentModelCreate>(product));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentModelCreate>> PutPayment(int id, PaymentModelCreate model)
        {
            try
            {
                var oldPayment = await _repository.GetPaymentByIdAsync(id);
                if (oldPayment == null) return NotFound($"Could not find Payment: {id}");

                _mapper.Map(model, oldPayment);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<PaymentModelCreate>(oldPayment);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var oldPayment = await _repository.GetPaymentByIdAsync(id);
                if (oldPayment == null) return NotFound();

                _repository.Delete(oldPayment);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted Payment {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The Payment");
        }
    }
}