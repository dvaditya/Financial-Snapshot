namespace FinancialSnapshot.Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public string Username { get; set; }
        public int LoginAttempts { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }

        public ICollection<UserPassword> UserPasswords { get; set; }
        public ICollection<UserSession> UserSessions { get; set; }
    }
}
