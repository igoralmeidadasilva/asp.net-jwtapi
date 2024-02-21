using Jwt.Project.Domain.Entities;

namespace Jwt.Project.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(Customer costumer);

}

