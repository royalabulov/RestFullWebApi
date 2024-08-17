namespace RestFullWebApi.DTOs
{
		public class TokenDTO
		{
			public string AccessToken { get; set; }
			public DateTime Expiration { get; set; } //lifetime
			public string RefreshToken { get; set; }
		}
	
}
