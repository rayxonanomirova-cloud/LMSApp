using LMSApp.Application.Interfaces;
using LMSApp.Domain.Models.LoginModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var token = await _authService.Login(login);
            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(token);
        }
    }
}
