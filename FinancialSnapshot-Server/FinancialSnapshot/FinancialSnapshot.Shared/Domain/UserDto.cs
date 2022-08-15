namespace FinancialSnapshot.Models.Domain
{
    public class UserDto
    {
        public UserDto()
        {
            UserPasswords = new List<UserPasswordDto>();
        }
        public int Id { get; set; }
        public string? Token { get; set; }
        public int LoginAttempts { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }

        public UserInfoDto UserInfoDto { get; set; }
        public IEnumerable<UserPasswordDto> UserPasswords { get; set; }
    }
}
