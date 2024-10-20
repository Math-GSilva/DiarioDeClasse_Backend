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
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var usuario = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (usuario == null)
            {
                return Unauthorized();
            }

            var token = _authService.GenerateJwtToken(usuario);

            return Ok(new { Token = token });
        }
    }
}
