using FinancialSnapshot.Models.Domain;
using FinancialSnapshot.Models.Web.General;
using FinancialSnapshot.Models.Web.Security;

namespace FinancialSnapshot.Abstraction.Services
{
    public interface IUserService
    {
        Task<BaseDataResponse<string>> ProcessLogin(string username, string password);
        Task<BaseDataResponse<bool>> ProcessRegister(RegisterRequest request);
    }
}
