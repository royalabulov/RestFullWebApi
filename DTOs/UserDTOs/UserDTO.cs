namespace RestFullWebApi.DTOs.UserDTOs
{

	public class CreateUserDto
	{
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public class CreateUserResponseDTo
	{
		public bool Succeeded { get; set; }
		public string Message { get; set; }
	}

	public class UserDTO
	{ 
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
	}

	public class UserUpdateDTO
	{
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
	}

}
