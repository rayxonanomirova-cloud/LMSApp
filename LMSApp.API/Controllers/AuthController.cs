using LMSApp.Application.Interfaces;
using LMSApp.Domain.Models.LoginModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSApp.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var token = await _authService.LoginAsync(login);
            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(token);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AccessToken([FromBody] string refreshToken)
        {
            var accessToken = await _authService.AccessTokenAsync(refreshToken);
            if (accessToken == null)
                return Unauthorized("Invalid refresh token.");

            return Ok(accessToken);
        }

        [HttpPut]
        public async Task<IActionResult> Logout()
        {
            return await _authService.GogoutAsync(userProfile.Id)
                ? Ok("Logged out successfully.") : BadRequest("Logout failed.");
        }
    }
}
