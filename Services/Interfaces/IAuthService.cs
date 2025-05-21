using Shopify.DTOs.AuthDTOs;
using Shopify.Models.Responses;
using Shopify.Shared;

namespace Shopify.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserRegistrationResponse>> RegisterAsync(RegisterDTO registerDTO);
        Task<Result<UserLoginResponse>> LoginAsync(LoginDTO loginDTO);
        Task<Result<UserLoginResponse>> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO);
        Task RevokeTokenAsync(RefreshTokenDTO refreshTokenDTO);
    }
}