using AutoMapper;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerRepository _repository;
        private readonly IMapper _mapper;

        public ManufacturerController(IManufacturerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ManufacturerModel[]>> GetManufacturers()
        {
            try
            {
                var allManufacturers = await _repository.GetAllManufacturersAsync();
                return _mapper.Map<ManufacturerModel[]>(allManufacturers);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerModel>> GetManufacturer(int id)
        {
            try
            {

                var result = await _repository.GetManufacturerAsync(id);

                if (result == null) return NotFound($"Could not find Manufacturer {id}");

                return _mapper.Map<ManufacturerModel>(result);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
