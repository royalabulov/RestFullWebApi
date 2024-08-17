using RestFullWebApi.DTOs.UserDTOs;
using RestFullWebApi.Entities.Identity;
using RestFullWebApi.Models;

namespace RestFullWebApi.Abstractions.IServices
{
	public interface IUserService
	{
		Task UpdateRefreshToken(string refreshToken,AppUser user,DateTime accessTokenDate);

		public Task<GenericResponseModel<bool>> AssignRoleToUserAsync(string userId, string[] roles);

		Task<GenericResponseModel<CreateUserResponseDTo>> CreateAsync(CreateUserDto model);//

		public Task<GenericResponseModel<List<UserDTO>>> GetAllUserAsync();//

		public Task<GenericResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName);

		public Task<GenericResponseModel<bool>> DeleteUserAsync(string userIdOrName);//

		public Task<GenericResponseModel<bool>> UpdateUserAsync(UserUpdateDTO model);//
	}
}
