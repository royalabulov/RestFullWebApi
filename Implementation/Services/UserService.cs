using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.DTOs.UserDTOs;
using RestFullWebApi.Entities.Identity;
using RestFullWebApi.Models;

namespace RestFullWebApi.Implementation.Services
{

	public class UserService : IUserService
	{
		private readonly IMapper mapper;
		private readonly UserManager<AppUser> userManager;

		public UserService(IMapper mapper, UserManager<AppUser> userManager)
		{
			this.mapper = mapper;
			this.userManager = userManager;
		}

		public async Task<GenericResponseModel<CreateUserResponseDTo>> CreateAsync(CreateUserDto model)
		{
			var response = new GenericResponseModel<CreateUserResponseDTo>();
			try
			{
				var id = Guid.NewGuid().ToString();

				if (model == null)
				{
					response.Data = null;
					response.StatusCode = 404;
				}

				var mappedUser = mapper.Map<AppUser>(model);
				mappedUser.Id = id;
				var result = await userManager.CreateAsync(mappedUser, model.Password);
				response.StatusCode = 200;
				response.Data = new CreateUserResponseDTo { Succeeded = result.Succeeded };

				if (!result.Succeeded)
				{
					response.StatusCode = 400;
					response.Data.Message = string.Join(" \n ", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
				}
				var user = (await userManager.FindByNameAsync(model.UserName) ?? await userManager.FindByEmailAsync(model.Email)) ??
							await userManager.FindByIdAsync(id);

				if (user != null)
					await userManager.AddToRoleAsync(user, "User");
			}
			catch (Exception ex)
			{
				await Console.Out.WriteLineAsync(ex.Message);
				response.StatusCode = 500;
			}
			return response;
		}

		public async Task<GenericResponseModel<List<UserDTO>>> GetAllUserAsync()
		{
			var response = new GenericResponseModel<List<UserDTO>>()
			{
				Data = null,
				StatusCode = 400,
			};

			try
			{
				var users = await userManager.Users.ToListAsync();

				if (users.Count > 0)
				{
					var mapping = mapper.Map<List<UserDTO>>(users);

					response.Data = mapping;
					response.StatusCode = 200;
				}

			}
			catch (Exception ex)
			{
				await Console.Out.WriteLineAsync(ex.Message);
				response.StatusCode = 500;
			}
			return response;
		}

		public async Task<GenericResponseModel<bool>> DeleteUserAsync(string userIdOrName)
		{
			var response = new GenericResponseModel<bool>();

			try
			{
				var getUserId = await userManager.FindByIdAsync(userIdOrName);
				if (getUserId == null)
				{
					getUserId = await userManager.FindByNameAsync(userIdOrName);
				}
				if (getUserId == null)
				{
					throw new ArgumentNullException(nameof(getUserId));
				}

				var deleteUser = await userManager.DeleteAsync(getUserId);

				if (deleteUser.Succeeded)
				{
					response.Data = true;
					response.StatusCode = 200;
				}

			}
			catch (Exception ex)
			{
				await Console.Out.WriteLineAsync(ex.Message);
				response.StatusCode = 500;
			}
			return response;
		}

		public async Task<GenericResponseModel<bool>> UpdateUserAsync(UserUpdateDTO model)
		{
			var response = new GenericResponseModel<bool>();

			try
			{
				var getUserId = await userManager.FindByIdAsync(model.UserId);

				var mapping = mapper.Map(model, getUserId);

				IdentityResult updateUser = await userManager.UpdateAsync(mapping);

				if (updateUser.Succeeded)
				{
					response.Data = updateUser.Succeeded;
					response.StatusCode = 200;
				}
				else
				{
					response.Data = updateUser.Succeeded;
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

		public async Task<GenericResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName)
		{
			var response = new GenericResponseModel<string[]>()
			{
				Data = null,
				StatusCode = 400
			};

			AppUser user = await userManager.FindByIdAsync(userIdOrName);

			if (user == null)
			{
				user = await userManager.FindByNameAsync(userIdOrName);
			}

			try
			{
				if (user != null)
				{
					var userRoles = await userManager.GetRolesAsync(user);
					response.StatusCode = 200;
					response.Data = userRoles.ToArray();
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

		public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate)
		{
			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.RefreshTokenEndTime = accessTokenDate.AddMinutes(10);
				await userManager.UpdateAsync(user);
			}
		}

		public async Task<GenericResponseModel<bool>> AssignRoleToUserAsync(string userId, string[] roles)
		{
			var response = new GenericResponseModel<bool>()
			{
				Data = false,
				StatusCode = 400,
			};

			AppUser user = await userManager.FindByIdAsync(userId);
			try
			{
				if(user != null)
				{
					var userRoles = await userManager.GetRolesAsync(user);
					await userManager.RemoveFromRolesAsync(user, userRoles);
					await userManager.AddToRolesAsync(user, roles);

					response.Data = true;
					response.StatusCode=200;

					return response;
				}
				else
				{
					return response;
				}
				
			}
			catch (Exception ex)
			{

			}
			return response;
		}

	}
}
