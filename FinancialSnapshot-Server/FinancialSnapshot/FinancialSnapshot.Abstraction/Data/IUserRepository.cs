using FinancialSnapshot.Models.Domain;

namespace FinancialSnapshot.Abstraction.Data
{
    public interface IUserRepository
    {
        bool CheckTokenExpiryByUserId(int id, byte type);
        Task<UserDto> GetUserByUsername(string username);
        Task<UserPasswordDto> GetUserHashByUsername(string username);
        Task<bool> RegisterUser(UserInfoDto userInfoDto, UserPasswordDto passwordDto);
        Task<bool> UpdateWebTokenToUser(int userId, string token, DateTime tokenExpiry);
    }
}
