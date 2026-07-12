using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.Authentication;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IIdentityService identityService , ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }
        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            var userResult = await _identityService.FindByEmailAsync(loginDto.Email , ct);
            if (!userResult.IsSuccess) return Result<UserDto>.Fail(userResult.Errors);

            var passResult = await _identityService.CheckPasswordAsync(loginDto.Email , loginDto.Password , ct);
            if (!passResult.IsSuccess) return Result<UserDto>.Fail(Error.Unauthorized("Invalid Email or Password"));

            return new UserDto
            {
                Email = userResult.data.Email,
                DisplayName = userResult.data.DisplayName,
                Token = "Token"
            };
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct = default)
        {
            var result = await _identityService.CreateUserAsync(registerDto, ct);
            if(!result.IsSuccess || result.data is null) return Result<UserDto>.Fail(result.Errors);

            return new UserDto
            {
                Email = result.data.Email,
                DisplayName = result.data.DisplayName,
                Token = "Token"
            };
        }
    }
}
