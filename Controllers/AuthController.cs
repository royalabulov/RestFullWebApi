using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.Models;

namespace RestFullWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService authService;

		public AuthController(IAuthService authService)
        {
			this.authService = authService;
		}

		[HttpPost]
		public async Task<IActionResult> Login(string username, string password)
		{
			var response = await authService.LoginAsync(username, password);
			return StatusCode(response.StatusCode, response);
		}

		[HttpPut("[action]")]
		[Authorize(Roles = "Admin,User")] //????????
		public async Task<IActionResult> LogOut(string userNameOfEmail)
		{
			var response = await authService.LogOut(userNameOfEmail);
			return StatusCode(response.StatusCode, response);
		}

		[HttpPost("password-reset-token")]
		[Authorize(Roles = "Admin,User")]
		public async Task<IActionResult> PasswordReset(string userNameOfEmail,string currentPas, string newPas)
		{
			var response = await authService.PasswordResetAsnyc(userNameOfEmail, currentPas, newPas);
			return StatusCode(response.StatusCode, response);
		}

		[HttpPost("refresh-token-login")]
		public async Task<IActionResult> LoginWithRefreshToken(string refreshToken)
		{
			var response = await authService.LoginWithRefreshTokenAsync(refreshToken);
			return StatusCode(response.StatusCode, response);
		}

	}
}
