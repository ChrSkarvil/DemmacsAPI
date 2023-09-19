using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using System.Security.Claims;

namespace DemmacsAPIv2.Data
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly DemmacsdbContext _context;
        public JwtTokenManager(IConfiguration configuration, DemmacsdbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public string Authenticate(string email, string password)
        {
            if (!_context.Logins.Any(x => x.Email.Equals(email)
                && x.Password.Equals(BC.HashPassword(password))))
                return null;

            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
                
        }
    }
}
