using DiarioDeClasse.Domain.DTOs.Auth;
using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Service;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiarioDeClasse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var usuario = await authService.AuthenticateAsync(request.Username, request.Password);

            if (usuario == null)
            {
                return Unauthorized();
            }

            var token = authService.GenerateJwtToken(usuario);

            return Ok(new { Token = token });
        }

        [HttpPost]
        [Route("VerificarToken")]
        public IActionResult ValidataToken([FromBody] string token)
        {
            bool valid = authService.ValidateToken(token);
            if (valid)
                return Ok(valid);

            return Unauthorized();
        }

        [HttpPost]
        [Route("IsAdmnistrador")]
        public async Task<IActionResult> IsAdm([FromBody] string token)
        {
            bool valid = authService.ValidateToken(token);
            
            if (valid)
            {
                bool isAdm = await authService.IsAdm(token);
                return Ok(isAdm);
            }

            return Unauthorized();
        }
    }
}
