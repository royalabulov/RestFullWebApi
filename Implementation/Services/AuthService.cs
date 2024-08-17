using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.DTOs;
using RestFullWebApi.Entities.Identity;
using RestFullWebApi.Models;

namespace RestFullWebApi.Implementation.Services
{
	public class AuthService : IAuthService
	{
		private readonly SignInManager<AppUser> signInManager;
		private readonly IUserService userService;
		private readonly ITokenHandler tokenHandler;
		private readonly UserManager<AppUser> userManager;

		public AuthService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHandler tokenHandler, IUserService userService)
		{
			this.signInManager = signInManager;
			this.userService = userService;
			this.tokenHandler = tokenHandler;
			this.userManager = userManager;
		}


		public async Task<GenericResponseModel<TokenDTO>> LoginAsync(string userNameOrEmail, string password)
		{
			AppUser appUser = await userManager.FindByNameAsync(userNameOrEmail);

			if (appUser == null)
			{
				appUser = await userManager.FindByEmailAsync(userNameOrEmail);
			}

			if(appUser == null)
			{
				return new()
				{
					Data = null,
					StatusCode = 400,
				};
			}

			SignInResult result = await signInManager.CheckPasswordSignInAsync(appUser, password, false);

			if(result.Succeeded)
			{
				TokenDTO token = await tokenHandler.CreateAccessToken(appUser);
				
				await userService.UpdateRefreshToken(token.RefreshToken,appUser,token.Expiration.AddMinutes(5));
				return new() {
				   Data = token,
				   StatusCode = 200,
				};
			}
			else
			{
				return new() { Data = null, StatusCode = 401 };

			}
		}

		public async Task<GenericResponseModel<TokenDTO>> LoginWithRefreshTokenAsync(string refreshToken)
		{
			AppUser user = await userManager.Users.FirstOrDefaultAsync(rf => rf.RefreshToken == refreshToken);

			if (user != null && user?.RefreshTokenEndTime > DateTime.UtcNow)
			{
				
				TokenDTO token = await tokenHandler.CreateAccessToken(user);
				await userService.UpdateRefreshToken(token.RefreshToken,user,token.Expiration.AddMinutes(5));
				return new()
				{
					Data = token,
					StatusCode = 200,
				};
			}
			else
			{
				return new()
				{
					Data = null,
					StatusCode = 401,
				};
			}
		}

		public async Task<GenericResponseModel<bool>> LogOut(string userNameOrEmail)
		{
			AppUser user = await userManager.FindByNameAsync(userNameOrEmail);

			if (user == null)
				user = await userManager.FindByEmailAsync(userNameOrEmail);

			if (user == null)
				return new()
				{
					Data = false,
					StatusCode = 400,
				};


			user.RefreshTokenEndTime = null;
			user.RefreshToken = null;

			var result = await userManager.UpdateAsync(user);
			await signInManager.SignOutAsync();

			if (result.Succeeded)
			{
				return new()
				{
					Data = true,
					StatusCode = 200,
				};
			}
			else
			{
				return new()
				{
					Data = false,
					StatusCode = 400
				};
			}
		}

		public async Task<GenericResponseModel<bool>> PasswordResetAsnyc(string email, string currentPas, string newPas)
		{
			GenericResponseModel<bool> response = new() { Data = false, StatusCode = 404 };
			AppUser user = await userManager.FindByEmailAsync(email);
			if (user != null)
			{
				var data = await userManager.ChangePasswordAsync(user, currentPas, newPas);

				if (data.Succeeded)
				{
					response.Data = true;
					response.StatusCode = 200;	
					return response;
				}
			}

			return response;
		}
	}
}
