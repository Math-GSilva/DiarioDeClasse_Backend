using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiarioDeClasse.Domain.Service
{
    public class AuthService
    {
        private readonly IUserRepository _usuarioRepository;
        private readonly string _jwtKey;

        public AuthService(IUserRepository usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _jwtKey = config["Jwt:Key"];
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var usuario = await _usuarioRepository.GetByUsernameAsync(username);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(password, usuario.Password))
            {
                return null;
            }

            return usuario;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtKey)),
            };

            try
            {
                handler.ValidateToken(token, parameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsAdm(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                bool isAdm = false;
                var jwtToken = handler.ReadJwtToken(token);
                var emailClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

                if(emailClaim != null)
                {
                    var user = await _usuarioRepository.GetByUsernameAsync(emailClaim.Value);
                    isAdm = (user?.Tipo ?? "") == "adm";
                }

                return isAdm;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
