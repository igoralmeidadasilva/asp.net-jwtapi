using System.Text.Json.Serialization;
using MediatR;

namespace Jwt.Project.Application.Commands.UpdateCustomer;

public sealed record UpdateCustomerPasswordCommand : IRequest<Unit>
{
    [JsonIgnore]
    public string Login { get; set; } = string.Empty; 
    public string Password { get; private set; } = string.Empty; 

    public UpdateCustomerPasswordCommand(string password)
    {
        Password = password;
    }

}
