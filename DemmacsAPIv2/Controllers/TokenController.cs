using DemmacsAPIv2.Data;
using DemmacsAPIv2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemmacsAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IJwtTokenManager _tokenManager;
        public TokenController(IJwtTokenManager jwtTokenManager)
        {
               _tokenManager = jwtTokenManager;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public ActionResult Authenticate(LoginCredential credential)
        {
            var token = _tokenManager.Authenticate(credential.Email, credential.Password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }
    }
}
