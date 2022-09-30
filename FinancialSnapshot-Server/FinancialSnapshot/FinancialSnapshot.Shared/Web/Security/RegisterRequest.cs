using FinancialSnapshot.Models.Domain;

namespace FinancialSnapshot.Models.Web.Security
{
    public class RegisterRequest
    {
        public string Password { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
