using Microsoft.AspNetCore.Identity;

namespace RestFullWebApi.Entities.Identity
{
	public class AppUser : IdentityUser<string>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenEndTime { get; set; }
	}
}
