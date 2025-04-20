using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TechStation.Data.DbContexts;
using TechStation.Domain.Entities;
using TechStation.Service.DTOs.Auths;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.Auths;

namespace TechStation.Service.Services.Auths;

public class AuthService : IAuthService
{
    private readonly AppDbContext appDbContext;
    private readonly IConfiguration configuration;

    public AuthService(IConfiguration configuration, AppDbContext appDbContext)
    {
        this.configuration = configuration;
        this.appDbContext = appDbContext;
    }
    public async Task<LoginResultDto> AuthenticateAsync(LoginDto dto)
    {
        var user = await appDbContext.Set<User>()
            .FirstOrDefaultAsync(u => u.UserName == dto.UserName && u.Password == dto.Password);

        if (user == null)
            throw new TechStationException(409, "User is invalid");

        var accessToken = GenerateToken(user);
        var refreshToken = GenerateRefreshToken();

        var tokenEntity = new RefreshToken
        {
            Token = refreshToken,
            ExpiryDate = DateTime.UtcNow.AddMinutes(5),
            UserId = user.Id,
        };

        this.appDbContext.Set<RefreshToken>().Add(tokenEntity);
        await this.appDbContext.SaveChangesAsync();

        return new LoginResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("Id", user.Id.ToString()),
                 new Claim("Email",user.Email),
                 new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Audience = configuration["JWT:Audience"],
            Issuer = configuration["JWT:Issuer"],
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["JWT:Expire"])),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber); // Tasodifiy 64-bayt uzunlikdagi refresh token
    }

    public async Task LogoutAsync(long userId)
    {
        var refreshTokens = await appDbContext.Set<RefreshToken>()
            .Where(t => t.UserId == userId && t.ExpiryDate > DateTime.UtcNow)
            .ToListAsync();

        if (!refreshTokens.Any())
            throw new InvalidOperationException("No active refresh tokens found for this user");

        appDbContext.Set<RefreshToken>().RemoveRange(refreshTokens);
        await appDbContext.SaveChangesAsync();
    }
}
