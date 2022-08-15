using FinancialSnapshot.Models.Domain;

namespace FinancialSnapshot.Models.Web.Security
{
    public class RegisterRequest
    {
        public string Password { get; set; }
        public UserInfoDto UserInfoDto { get; set; }
    }
}
