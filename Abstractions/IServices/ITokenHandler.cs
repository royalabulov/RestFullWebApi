using RestFullWebApi.DTOs;
using RestFullWebApi.Entities.Identity;

namespace RestFullWebApi.Abstractions.IServices
{
	public interface ITokenHandler
	{
		Task<TokenDTO> CreateAccessToken(AppUser user);
		string CreateRefreshToken();

	}
}
