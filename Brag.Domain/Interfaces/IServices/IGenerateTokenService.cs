
using Brag.Domain.Dtos.RequestParams;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Brag.Domain.Interfaces.IServices
{
    public interface IGenerateTokenService
    {
        Task<string> CreateUserToken(string Email, string AuthId); 
        Task<string> CreateUserTokenAsync(dynamic param);
        Task<bool> IsTokenValid(string authToken); //Check if the token is valid
        Task<TokenParams> GetUserTokenParams(string authToken);
        Task<ClaimsPrincipal> ValidateToken_GetTokenParam(string jwtToken);

        

        Task<string> CreateUserRoleToken(IDictionary<string, object> authClaims, string AuthId,string RoleName,string Email);
    }
}
