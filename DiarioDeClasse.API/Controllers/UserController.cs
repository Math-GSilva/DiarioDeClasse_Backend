using DiarioDeClasse.Domain.DTOs.Auth;
using DiarioDeClasse.Domain.Entity;
using DiarioDeClasse.Domain.Interface.Services;
using DiarioDeClasse.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace DiarioDeClasse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] User usuario)
        {
            return Ok(userService.SaveUsuarioAsync(usuario));
        }
    }
}
