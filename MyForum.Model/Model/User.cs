namespace MyForum.Model
{
	public class User : IEntity
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string? Avatar { get; set; }
		public string Password { get; set; }
		public string? Email { get; set; }
		public string? Website { get; set; }
		public string? Bio { get; set; }
		public int? Score { get; set; } 
		public DateTime InTime { get; set; }
		public bool IsAdmin { get; set; }	
	}
}
