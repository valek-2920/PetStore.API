using Microsoft.IdentityModel.Tokens;
using Pet_Store.Domain.Models.PlainModels;

namespace Pet_Store.Application.Contracts.Managers
{
    public interface IJwtManager
    {
        JwtToken Authenticate(JwtUser input);

        bool IsTokenValid(string token, out SecurityToken authorizedToken);
    }
}
