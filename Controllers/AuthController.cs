using Microsoft.AspNetCore.Mvc;
using Shopify.DTOs.AuthDTOs;
using Shopify.Services.Interfaces;

namespace Shopify.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        private readonly IAuthService authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var result = await authService.RegisterAsync(registerDTO);

            if(result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);

            return StatusCode(result.StatusCode, new { error = result.Error });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var result = await authService.LoginAsync(loginDTO);

            if(result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);

            return StatusCode(result.StatusCode, new { error = result.Error });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var result = await authService.RefreshTokenAsync(refreshTokenDTO);

            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);

            return StatusCode(result.StatusCode, new { error = result.Error });
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            await authService.RevokeTokenAsync(refreshTokenDTO);

            return NoContent();
        }

    }
}