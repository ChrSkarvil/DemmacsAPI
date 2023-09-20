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
        private readonly IServiceProvider _serviceProvider;
        public JwtTokenManager(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public string Authenticate(string email, string password)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DemmacsdbContext>();

                // Find the user by email
                var user = context.Logins.SingleOrDefault(x => x.Email.Equals(email));

                // Check if a user with the given email exists
                if (user == null)
                {
                    return null; // User not found
                }

                // Use BCrypt.Verify to check if the provided password matches the stored BCrypt-hashed password
                if (!BC.Verify(password, user.Password))
                {
                    try
                    {
                        return null; // Passwords do not match
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }

                var key = _configuration.GetValue<string>("JwtConfig:SecretKey");
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
}
