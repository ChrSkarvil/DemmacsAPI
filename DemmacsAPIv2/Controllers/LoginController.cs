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
        public async Task<ActionResult<LoginModel[]>> GetLogins()
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

        [HttpGet("{email}")]
        public async Task<ActionResult<LoginModel>> GetLogin(string email)
        {
            try
            {

                var result = await _repository.GetLoginAsync(email);

                if (result == null) return NotFound($"Could not find login {email}");

                return _mapper.Map<LoginModel>(result);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LoginModelCreate>> PostLogin(LoginModelCreate model)
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

        [HttpPut("{email}")]
        public async Task<ActionResult<LoginModelCreate>> PutLogin(string email, LoginModelCreate model)
        {
            try
            {
                var oldLogin = await _repository.FindLogin(email);
                if (oldLogin == null) return NotFound($"Could not find Login {email}");

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
        public async Task<IActionResult> DeleteLogin(string email)
        {
            try
            {
                var oldLogin = await _repository.FindLogin(email);
                if (oldLogin == null) return NotFound();

                _repository.Delete(oldLogin);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok($"Successfully Deleted Login {email}");
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
