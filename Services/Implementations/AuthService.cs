using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopify.DTOs.AuthDTOs;
using Shopify.Models;
using Shopify.Models.Responses;
using Shopify.Services.Interfaces;
using Shopify.Shared;
using System.IdentityModel.Tokens.Jwt;

namespace Shopify.Services.Implementations
{
    public class AuthService(UserManager<ShopifyUser> userManager, ITokenProviderService tokenProviderService, IConfiguration configuration) : IAuthService
    {

        private readonly UserManager<ShopifyUser> userManager = userManager;
        private readonly ITokenProviderService tokenProviderService = tokenProviderService;
        private readonly IConfiguration configuration = configuration;

        public async Task<Result<UserRegistrationResponse>> RegisterAsync(RegisterDTO registerDTO)
        {
            if (await userManager.FindByNameAsync(registerDTO.Username) is not null)
                return Result<UserRegistrationResponse>.Failure("Username is already registered.", 409);

            if(await userManager.FindByEmailAsync(registerDTO.Email) is not null)
                return Result<UserRegistrationResponse>.Failure("Email is already registered.", 409);

            var user = new ShopifyUser()
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if(!result.Succeeded)
                return Result<UserRegistrationResponse>.Failure(string.Join(" | ", result.Errors.Select(e => e.Description)), 400);

            return Result<UserRegistrationResponse>.Success(new UserRegistrationResponse
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            }, 201);
        }

        public async Task<Result<UserLoginResponse>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await userManager.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, loginDTO.Password))
                return Result<UserLoginResponse>.Failure("Email or Password is incorrect!", 401);

            var accessToken = await tokenProviderService.GetAccessTokenAsync(user);
            var refreshToken = user.RefreshTokens.FirstOrDefault(r => !r.IsRevoked && DateTime.Now < r.ExpiresAt);

            if (refreshToken is null)
            {
                refreshToken = new RefreshToken
                {
                    Id = Guid.NewGuid().ToString(),
                    Token = tokenProviderService.GetRefreshToken(),
                    CreatedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddDays(Convert.ToDouble(configuration["RefreshToken:ExpiryDays"])),
                    IsRevoked = false,
                    UserId = user.Id
                };
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(user);
            }

            return Result<UserLoginResponse>.Success(new UserLoginResponse
                {
                    Email = loginDTO.Email,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    RefreshToken = refreshToken.Token,
                    RefreshTokenExpiration = refreshToken.ExpiresAt
                }, 200);
        }

        public async Task<Result<UserLoginResponse>> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            var user = await userManager.Users.Include(u => u.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshTokenDTO.Token));
            
            if (user is null)
                return Result<UserLoginResponse>.Failure("Invalid token", 401);

            var refreshToken = user.RefreshTokens.Single(r => r.Token == refreshTokenDTO.Token);

            if (!(!refreshToken.IsRevoked && DateTime.Now < refreshToken.ExpiresAt))
                return Result<UserLoginResponse>.Failure("Inactive token", 401);

            refreshToken.IsRevoked = true;

            var newRefreshToken = new RefreshToken
            {
                Id = Guid.NewGuid().ToString(),
                Token = tokenProviderService.GetRefreshToken(),
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(Convert.ToDouble(configuration["RefreshToken:ExpiryDays"])),
                IsRevoked = false,
                UserId = user.Id
            }; 
            user.RefreshTokens.Add(newRefreshToken);
            await userManager.UpdateAsync(user);

            var newAccessToken = await tokenProviderService.GetAccessTokenAsync(user);

            return Result<UserLoginResponse>.Success(new UserLoginResponse
            {
                Email = user.Email!,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiresAt
            }, 200);
        }

        public async Task RevokeTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            var user = await userManager.Users.Include(u => u.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshTokenDTO.Token));

            if (user is null)
                return;

            var refreshToken = user.RefreshTokens.Single(r => r.Token == refreshTokenDTO.Token);

            if (!(!refreshToken.IsRevoked && DateTime.Now < refreshToken.ExpiresAt))
                return;

            refreshToken.IsRevoked = true;
            await userManager.UpdateAsync(user);
        }

    }
}