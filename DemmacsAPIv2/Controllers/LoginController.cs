using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using DemmacsAPIv2.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using BC = BCrypt.Net.BCrypt;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginRepository _repository;
        private readonly IMapper _mapper;

        public LoginController(ILoginRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<LoginModel[]>> Get()
        {
            try
            {
                var allLogins = await _repository.GetAllLoginsAsync();
                return _mapper.Map<LoginModel[]>(allLogins);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoginModel>> Get(int id)
        {
            try
            {

                var result = await _repository.GetLoginAsync(id);

                if (result == null) return NotFound($"Could not find login {id}");

                return _mapper.Map<LoginModel>(result);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LoginModelCreate>> Post(LoginModelCreate model)
        {
            try
            {
                //Create a new Login
                var login = _mapper.Map<Login>(model);
                login.Password = BC.HashPassword(model.Password);
                _repository.Add(login);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/Login/{login.LoginId}", _mapper.Map<LoginModelCreate>(login));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LoginModelCreate>> Put(int id, LoginModelCreate model)
        {
            try
            {
                var oldLogin = await _repository.FindLogin(id);
                if (oldLogin == null) return NotFound($"Could not find Login {id}");

                model.Password = BC.HashPassword(model.Password);

                _mapper.Map(model, oldLogin);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<LoginModelCreate>(oldLogin);
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldLogin = await _repository.FindLogin(id);
                if (oldLogin == null) return NotFound();

                _repository.Delete(oldLogin);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted Login {id}");
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest("Failed To Delete The Login");
        }
    }
}
