
using Brag.Domain.Dtos.RequestParams;
using Brag.Domain.Exceptions;
using Brag.Domain.Interfaces.IServices;
using Brag.Domain.Model.Configuration;
using Brag.SharedServices.Statics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Brag.SharedServices.Services.TokenService
{
    public class GenerateTokenService : IGenerateTokenService
    {
        private readonly AppSettings _appSettings;
        private readonly SymmetricSecurityKey _key;
        public GenerateTokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtKey));
        }

        public Task<string> CreateUserRoleToken(IDictionary<string,object> authClaims, string AuthId, string RoleName,string Email)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
               new[] { new Claim("AuthzId", AuthId),

               new Claim("eml", Email),
               new Claim(ClaimTypes.Role, RoleName) }); //or add Claim("UserRole",RoleName)
        
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
               // Claims =authClaims,
                Expires = GetLocalDateTime.CurrentDateTime().AddHours(4),
                SigningCredentials = creds,
                IssuedAt = GetLocalDateTime.CurrentDateTime(),

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(tokenHandler.WriteToken(token));
            
        }

        public Task<string> CreateUserToken(string Email, string AuthId)
        {
              ClaimsIdentity identity = new ClaimsIdentity(
               new[] { new Claim("eml", Email),
                new Claim("AuthzId", AuthId),
              });
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                
                Expires = GetLocalDateTime.CurrentDateTime().AddMinutes(120),
                SigningCredentials = creds,
                IssuedAt = GetLocalDateTime.CurrentDateTime(),
            
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(tokenHandler.WriteToken(token));
        }

        public Task<string> CreateUserTokenAsync(dynamic param)
        {
            throw new NotImplementedException();
        }

        public async  Task<TokenParams> GetUserTokenParams(string authToken)
        {
            var res= new TokenParams();
            var ClaimsPrincipal = ValidateToken_GetTokenParam(authToken).Result;
            if (ClaimsPrincipal == null) throw new UnauthorisedException("Invalid auth credentials");

            //get Claims from token
            res.Email =
                ClaimsPrincipal?.FindFirst("eml")?.Value;
            
            res.AuthId  =
               ClaimsPrincipal?.FindFirst("AuthzId")?.Value;
            
            return res;
        }

        

        public Task<bool> IsTokenValid(string authToken)
        {

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                return Task.FromResult(true);

            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }

        public async Task<ClaimsPrincipal> ValidateToken_GetTokenParam(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,

            };
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);


            return principal;
        }
    }
}
