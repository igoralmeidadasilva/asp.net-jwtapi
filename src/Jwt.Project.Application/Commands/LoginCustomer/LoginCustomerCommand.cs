using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Jwt.Project.Application.Commands.LoginCustomer;

public sealed class LoginCustomerCommand(string Login, string Password) : IRequest<string>
{
    [Required]
    public string Login { get; private set; } = Login;
    [Required]
    public string Password { get; private set; } = Password;
}
