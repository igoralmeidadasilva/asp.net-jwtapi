using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Jwt.Project.Domain.Entities;
using Jwt.Project.Domain.Interfaces.Services;
using Jwt.Project.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Project.Infrastructure.Services;

public sealed class TokenService : ITokenService
{
    private readonly IOptions<JwtAuthenticationOptions> _jwtOptions;

    public TokenService(IOptions<JwtAuthenticationOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public string GenerateToken(Customer customer)
    {
        // var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.Key!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, customer.Name.ToString()),
                new Claim(ClaimTypes.Role, customer.Role.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(_jwtOptions.Value.ExpiredAt),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
