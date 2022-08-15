using FinancialSnapshot.Abstraction.Data;
using FinancialSnapshot.Abstraction.Services;
using FinancialSnapshot.Common.Cryptography;
using FinancialSnapshot.Models.Configuration;
using FinancialSnapshot.Models.Domain;
using FinancialSnapshot.Models.Web.General;
using FinancialSnapshot.Models.Web.Security;
using System.Security.Claims;

namespace FinancialSnapshot.Common.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repo;
        private readonly TokenConfiguration _config;

        public UserService(IUserRepository repo, TokenConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<BaseDataResponse<string>> ProcessLogin(string username, string password)
        {
            var user = await _repo.GetUserByUsername(username);
            if(user == null || !user.UserPasswords.Any() || user.UserInfoDto == null)
                return BaseDataResponse<string>.Error("Invalid user!");

            var userPassword = user.UserPasswords.First();
            if (CryptographyProcessor.AreEqual(password, userPassword.Hash, userPassword.Salt))
            {
                var tokenExpiry = DateTime.MaxValue;
                var claims = new Claim[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("username", user.UserInfoDto.Username),
                    new Claim("email", user.UserInfoDto.Email),
                    new Claim("firstName", user.UserInfoDto.FirstName),
                    new Claim("middleName", user.UserInfoDto.MiddleName),
                    new Claim("lastName", user.UserInfoDto.LastName)
                };
                var token = JwtTokenHelper.GenerateJwtToken(claims, tokenExpiry, _config);

                var tokenUpdated = await _repo.UpdateWebTokenToUser(user.Id, token, tokenExpiry);

                if(tokenUpdated)
                    return BaseDataResponse<string>.Success(data: token);

                return BaseDataResponse<string>.Error("There was an error during login process. Please try again!");
            }
                

            return BaseDataResponse<string>.Error("Incorrect password. Please try again!");
        }

        public async Task<BaseDataResponse<bool>> ProcessRegister(RegisterRequest request)
        {
            if (request == null || request.UserInfoDto == null)
                return BaseDataResponse<bool>.Error(message: "Invalid request");

            var salt = CryptographyProcessor.CreateSalt(32);
            var hash = CryptographyProcessor.GenerateHash(request.Password, salt);

            var response = await _repo.RegisterUser(request.UserInfoDto, new UserPasswordDto { Salt = salt, Hash = hash });

            if(response)
                return BaseDataResponse<bool>.Success();
            else
                return BaseDataResponse<bool>.Error("Unable to register user. Please try again!");
        }
    }
}
