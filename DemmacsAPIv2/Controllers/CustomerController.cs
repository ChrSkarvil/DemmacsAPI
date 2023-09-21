using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerModel[]>> GetCustomers()
        {
            try
            {
                var allCustomers = await _repository.GetAllCustomersAsync();
                return _mapper.Map<CustomerModel[]>(allCustomers);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomer(int id)
        {
            try
            {

                var result = await _repository.GetCustomerByIdAsync(id);

                if (result == null) return NotFound($"Could not find customer {id}");

                return _mapper.Map<CustomerModel>(result);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CustomerModelCreate>> PostCustomer(CustomerModelCreate model)
        {
            try
            {
                //Create a new Customer
                var customer = _mapper.Map<Customer>(model);
                _repository.Add(customer);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/Customer/{customer.CustomerId}", _mapper.Map<CustomerModelCreate>(customer));
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerModelCreate>> PutProduct(int id, CustomerModelCreate model)
        {
            try
            {
                var oldCustomer = await _repository.GetCustomerByIdAsync(id);
                if (oldCustomer == null) return NotFound($"Could not find customer {id}");

                _mapper.Map(model, oldCustomer);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CustomerModelCreate>(oldCustomer);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var oldCustomer = await _repository.GetCustomerByIdAsync(id);
                if (oldCustomer == null) return NotFound();

                _repository.Delete(oldCustomer);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted Customer {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The Customer");
        }
    }
}
