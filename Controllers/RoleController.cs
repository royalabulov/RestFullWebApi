using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.DTOs.RoleDTOs;

namespace RestFullWebApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService roleService;

		public RoleController(IRoleService roleService)
		{
			this.roleService = roleService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var response = await roleService.GetAllRoles();
			return StatusCode(response.StatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> Create(string roleName)
		{
			var response = await roleService.CreateRole(roleName);
			return StatusCode(response.StatusCode, response);
		}

		[HttpPut]
		public async Task<IActionResult> Update(RoleUpdateDTO roleUpdateDTO)
		{
			var response = await roleService.UpdateRole(roleUpdateDTO);
			return StatusCode(response.StatusCode, response);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(string Id)
		{
			var response = await roleService.DeleteRole(Id);
			return StatusCode(response.StatusCode, response);
		}

		[HttpGet]
		public async Task<IActionResult> GetById(string Id)
		{
			var response = await roleService.GetRolesById(Id);
			return StatusCode(response.StatusCode, response);
		}
	}
}
