using System.ComponentModel.DataAnnotations;
using Jwt.Project.Domain.Enums;
using MediatR;

namespace Jwt.Project.Application.Commands.CreateCustomer
{
    public sealed record CreateCustomerCommand : IRequest<Unit>
    {
        [Required]
        public string Name { get; set; } = string.Empty; 
        [Required]
        public string Login { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public CustomerRole Role { get; set; }
    }
}
