using Business.Authentication;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromForm]RegisterAuthDto authDto)
        {
            var result = _authService.Register(authDto);
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginAuthDto loginDto)
        {
            var result = _authService.Login(loginDto);
            return Ok(result);
        }
    }
}
