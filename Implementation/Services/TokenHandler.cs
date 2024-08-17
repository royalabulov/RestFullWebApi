using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestFullWebApi.Abstractions.IServices;
using RestFullWebApi.DTOs;
using RestFullWebApi.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestFullWebApi.Implementation.Services
{
	public class TokenHandler : ITokenHandler
	{
		private readonly IConfiguration configuration;
		private readonly UserManager<AppUser> userManager;

		public TokenHandler(IConfiguration _configuration, UserManager<AppUser> userManager)
        {
			configuration = _configuration;
			this.userManager = userManager;
		}


		public async Task<TokenDTO> CreateAccessToken(AppUser user)
		{
			TokenDTO tokenDTO = new TokenDTO();

			
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

		
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
			{
         
                new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Email, user.Email)
			};

	
			var roles = await userManager.GetRolesAsync(user);
			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


	
			tokenDTO.Expiration = DateTime.UtcNow.AddMinutes(5);
			JwtSecurityToken securityToken = new(
				audience: configuration["Token:Audience"],
				issuer: configuration["Token:Issuer"],
				expires: tokenDTO.Expiration, 
				notBefore: DateTime.UtcNow, 
				signingCredentials: signingCredentials,
				claims: claims
				);

			
			JwtSecurityTokenHandler tokenHandler = new();
		
			tokenDTO.AccessToken = tokenHandler.WriteToken(securityToken);

			tokenDTO.RefreshToken = CreateRefreshToken();

			return tokenDTO;
		}

		public string CreateRefreshToken()
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(configuration["Token:RefreshTokenSecret"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var refreshToken = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(refreshToken);
		}
	}
}
