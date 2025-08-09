using Application.Services;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUserService _loginUserService;
        private readonly RegisterUserService _registerUserService;
        public AuthController(LoginUserService loginUserService, RegisterUserService registerUserService)
        {
            _loginUserService = loginUserService;
            _registerUserService = registerUserService;
        }
        [HttpPost("Login")]
        //[Authorize]
        public async Task<IActionResult> LoginAsync([FromBody] LoginVm request)
        {
            try
            {
                var token = await _loginUserService.LoginAsync(request.Email, request.Password);
                return Ok(new { token });

            }
            catch (Exception ex)
            {
                return Unauthorized(new { ex.Message });

            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAcync([FromBody] RegisterdVm request)
        {
            try
            {
                await _registerUserService.RegisterAsync(request.email, request.username,
                    request.password,request.userRole);
                return Ok(new { message="user registred successfully"});

            }
            catch (Exception ex)
            {
                return BadRequest(new { error=ex.Message });

            }
        }
        //This line defines an immutable class named LoginVm with two properties: Email and Password.
        //It is available in C# 9.0 and later.
        public record LoginVm(string Email, string Password);
        public record RegisterdVm(string email, string username, string password, UserRole userRole);
    }
}
