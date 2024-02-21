namespace Jwt.Project.Domain.Options;

public sealed record JwtAuthenticationOptions
{
    public string? Key { get; init; }
}
