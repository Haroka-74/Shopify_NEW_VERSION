using Shopify.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Shopify.Services.Interfaces
{
    public interface ITokenProviderService
    {
        public string GetRefreshToken();
        public Task<JwtSecurityToken> GetAccessToken(ShopifyUser user);
    }
}