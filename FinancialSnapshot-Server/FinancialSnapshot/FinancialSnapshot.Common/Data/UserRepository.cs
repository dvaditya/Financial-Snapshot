using FinancialSnapshot.Abstraction.Data;
using FinancialSnapshot.Common.Enums;
using FinancialSnapshot.Database;
using FinancialSnapshot.Database.Models;
using FinancialSnapshot.Models.Domain;
using FinancialSnapshot.Models.Web.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialSnapshot.Common.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserPasswordDto> GetUserHashByUsername(string username)
        {
            return await _context.UserPasswords
                .Where(x => x.User.Username == username &&
                            x.IsActive)
                .Select(x => new UserPasswordDto
                {
                    Username = x.User.Username,
                    Salt = x.Salt,
                    Hash = x.Hash
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UserDto> GetUserByUsername(string username)
        {
            return await _context.Users
                .Include(u => u.UserPasswords)
                .Where(x => x.Username == username &&
                            x.IsActive &&
                            !x.IsLocked)
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    UserInfoDto = new UserInfoDto
                    {
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        MiddleName = x.MiddleName,
                        Username = x.Username,
                    },                    
                    UserPasswords = x.UserPasswords.Where(p => p.IsActive).Select(p => new UserPasswordDto
                    {
                        Hash = p.Hash,
                        Salt = p.Salt
                    })
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateWebTokenToUser(int userId, string token, DateTime tokenExpiry)
        {
            try
            {
                var userSessions = _context.UserSessions.Where(x => x.UserId == userId && x.Type == (byte)UserSessionTypeEnum.Web).ToList();

                _context.UserSessions.RemoveRange(userSessions);

                var newSession = new UserSession
                {
                    UserId = userId,
                    Created = DateTime.UtcNow,
                    Token = token,
                    TokenExpiry = tokenExpiry,
                    Type = (byte)UserSessionTypeEnum.Web
                };

                _context.UserSessions.Add(newSession);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckTokenExpiryByUserId(int id, byte type)
        {
            return _context.UserSessions.FirstOrDefault(x => x.UserId == id && x.Type == type && x.TokenExpiry <= DateTime.UtcNow) != null;
        }

        public async Task<bool> RegisterUser(UserInfoDto userInfoDto, UserPasswordDto passwordDto)
        {
            using var transaction = _context.Database.BeginTransaction();
            try {
                var user = new User
                {
                    FirstName = userInfoDto.FirstName,
                    LastName = userInfoDto.LastName,
                    MiddleName = userInfoDto.MiddleName,
                    Created = DateTime.UtcNow,
                    Email = userInfoDto.Email,
                    IsActive = true,
                    IsLocked = false,
                    LoginAttempts = 0,
                    Username = userInfoDto.Username
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userPassword = new UserPassword
                {
                    Created = DateTime.UtcNow,
                    Hash = passwordDto.Hash,
                    IsActive = true,
                    Salt = passwordDto.Salt,
                    UserId = user.Id
                };
                _context.UserPasswords.Add(userPassword);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch(Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
