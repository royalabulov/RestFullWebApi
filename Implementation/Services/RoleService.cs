using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestFullWebApi.Abstractions;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.DTOs.RoleDTOs;
using RestFullWebApi.Entities.Identity;
using RestFullWebApi.Models;

namespace RestFullWebApi.Implementation.Services
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<AppRole> roleManager;
		private readonly IMapper mapper;

		public RoleService(RoleManager<AppRole> roleManager, IMapper mapper)
		{
			this.roleManager = roleManager;
			this.mapper = mapper;
		}

		public async Task<GenericResponseModel<bool>> CreateRole(string name)
		{
			var response = new GenericResponseModel<bool>()
			{
				Data = false,
				StatusCode = 400
			};

			try
			{
				IdentityResult result = await roleManager.CreateAsync(new AppRole { Name = name, Id = Guid.NewGuid().ToString() });
				if (result.Succeeded)
				{
					response.Data = result.Succeeded;
					response.StatusCode = 200;
				}
				else
				{
					response.Data = result.Succeeded;
					response.StatusCode = 200;
				}

			}
			catch (Exception ex)
			{
				response.Data = false;
				response.StatusCode = 500;
				await Console.Out.WriteLineAsync(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseModel<bool>> DeleteRole(string id)
		{
			var response = new GenericResponseModel<bool>()
			{
				Data = false,
				StatusCode = 400

			};


			try
			{
				var role = await roleManager.FindByIdAsync(id);
				IdentityResult result = await roleManager.DeleteAsync(role);

				if (result.Succeeded)
				{
					response.Data = result.Succeeded;
					response.StatusCode = 200;
				}
				else
				{
					response.Data = result.Succeeded;
					response.StatusCode = 400;
				}

			}
			catch (Exception ex)
			{
				response.Data = false;
				response.StatusCode = 500;
				await Console.Out.WriteLineAsync(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseModel<List<RoleGetDTO>>> GetAllRoles()
		{
			var response = new GenericResponseModel<List<RoleGetDTO>>()
			{
				Data = null,
				StatusCode = 400
			};

			try
			{
				var result = await roleManager.Roles.ToListAsync();
				if (result != null && result.Count > 0)
				{
					var mapping = mapper.Map<List<RoleGetDTO>>(result);
					response.Data = mapping;
					response.StatusCode = 200;
				}
			}
			catch (Exception ex)
			{
				response.Data = null;
				response.StatusCode = 500;
				await Console.Out.WriteLineAsync(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseModel<RoleGetDTO>> GetRolesById(string Id)
		{
			var response = new GenericResponseModel<RoleGetDTO>()
			{
				StatusCode = 400,
				Data = null

			};

			try
			{
				var roleId = await roleManager.FindByIdAsync(Id);


				if (roleId != null)
				{
					var mapping = mapper.Map<RoleGetDTO>(roleId);

					response.Data = mapping;
					response.StatusCode = 200;

				}
			}
			catch (Exception ex)
			{
				response.Data = null;
				response.StatusCode = 500;
				await Console.Out.WriteLineAsync(ex.Message);
			}
			return response;
		}

		public async Task<GenericResponseModel<bool>> UpdateRole(RoleUpdateDTO roleUpdateDTO)
		{
			var response = new GenericResponseModel<bool>()
			{
				Data = false,
				StatusCode = 400
			};

			try
			{
				var getRoleId = await roleManager.FindByIdAsync(roleUpdateDTO.Id.ToString());

				var mapping = mapper.Map(roleUpdateDTO, getRoleId);

				IdentityResult result = await roleManager.UpdateAsync(mapping);

				if (result.Succeeded)
				{
					response.Data = result.Succeeded;
					response.StatusCode = 200;
				}
				else
				{
					response.Data = result.Succeeded;
					response.StatusCode = 400;
				}

			}
			catch (Exception ex)
			{
				response.Data = false;
				response.StatusCode = 500;
				await Console.Out.WriteLineAsync(ex.Message);
			}
			return response;
		}
	}
}
