using RestFullWebApi.DTOs;
using RestFullWebApi.Models;

namespace RestFullWebApi.Abstractions.IServices
{
	public interface IAuthService
	{
		Task<GenericResponseModel<TokenDTO>> LoginAsync(string userNameOrEmail, string password);
		Task<GenericResponseModel<TokenDTO>> LoginWithRefreshTokenAsync(string refreshToken);
		Task<GenericResponseModel<bool>> LogOut(string userNameOrEmail);
		public Task<GenericResponseModel<bool>> PasswordResetAsnyc(string email, string currentPas, string newPas);
	}
}
