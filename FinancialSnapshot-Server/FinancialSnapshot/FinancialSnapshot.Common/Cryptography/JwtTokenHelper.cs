using FinancialSnapshot.Models.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FinancialSnapshot.Common.Cryptography
{
    public static class JwtTokenHelper
    {
        //public static string Issuer { get; } = Guid.NewGuid().ToString();
        //public static SecurityKey SecurityKey { get; }
        //public static SigningCredentials SigningCredentials { get; }

        private static readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        //private static readonly RandomNumberGenerator randomNumber = RandomNumberGenerator.Create();
        private static readonly byte[] keyLength = new byte[32];

        //static JwtTokenHelper()
        //{
        //    randomNumber.GetBytes(keyLength);
        //    SecurityKey = new SymmetricSecurityKey(keyLength) { KeyId = Guid.NewGuid().ToString() };
        //    SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        //}

        public static string GenerateJwtToken(IEnumerable<Claim> claims, DateTime tokenExpiry, TokenConfiguration tokenConfiguration)
        {
            //var issuer = tokenConfiguration.Issuer;
            //var keyLength = new byte[32];
            var securityKey = new SymmetricSecurityKey(keyLength) { KeyId = tokenConfiguration.SecurityKey };
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            return jwtSecurityTokenHandler.WriteToken(new JwtSecurityToken(tokenConfiguration.Issuer, null, claims, null, tokenExpiry, signingCredentials));
        }

        public static ClaimsPrincipal GenerateClaimsPrincipalFromToken(string token, TokenConfiguration tokenConfiguration)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,

                ValidAudience = null,
                ValidIssuer = tokenConfiguration.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(keyLength) { KeyId = tokenConfiguration.SecurityKey }
            };
            return jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out _);
        }
    }
}
