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
    private IConfiguration _configuration;
    private readonly IOptions<JwtAuthenticationOptions> _jwtOptions;

    public TokenService(IConfiguration configuration, IOptions<JwtAuthenticationOptions> jwtOptions)
    {
        _configuration = configuration;
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
                new Claim(ClaimTypes.Name, customer.Name)
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
