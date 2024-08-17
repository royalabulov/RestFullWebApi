using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.DTOs.UserDTOs;

namespace RestFullWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService userService;

		public UserController(IUserService userService)
		{
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var response = await userService.GetAllUserAsync();
			return StatusCode(response.StatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateUserDto createUserDto)
		{
			var response = await userService.CreateAsync(createUserDto);
			return StatusCode(response.StatusCode, response);
		}

		[HttpPut]
		public async Task<IActionResult> Update(UserUpdateDTO updateUserDto)
		{
			var response = await userService.UpdateUserAsync(updateUserDto);
			return StatusCode(response.StatusCode, response);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(string id)
		{
			var response = await userService.DeleteUserAsync(id);
			return StatusCode(response.StatusCode, response);
		}

		[HttpGet]
		public async Task<IActionResult> GetRolesToUser(string id)
		{
			var response = await userService.GetRolesToUserAsync(id);
			return StatusCode(response.StatusCode, response);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AssignRoleToUserAsync(string userId, string[] roles)
		{
			var response = await userService.AssignRoleToUserAsync($"{userId}", roles);
			return StatusCode(response.StatusCode, response);
		}

	}
}
