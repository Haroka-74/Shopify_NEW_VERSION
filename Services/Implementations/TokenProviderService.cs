using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shopify.Models;
using Shopify.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Shopify.Services.Implementations
{
    public class TokenProviderService(UserManager<ShopifyUser> userManager, IConfiguration configuration) : ITokenProviderService
    {

        private readonly UserManager<ShopifyUser> userManager = userManager;
        private readonly IConfiguration configuration = configuration;

        public string GetRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        public async Task<JwtSecurityToken> GetAccessTokenAsync(ShopifyUser user)
        {
            var claims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName!));

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessToken = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["JWT:ExpiryMinutes"])),
                signingCredentials: signingCredentials
            );

            return accessToken;
        }

    }
}