using DemmacsAPIv2.Data;
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
    }
}
