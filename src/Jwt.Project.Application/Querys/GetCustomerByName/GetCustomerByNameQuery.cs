using Jwt.Project.Domain.Entities;
using MediatR;

namespace Jwt.Project.Application.Querys.GetCustomerByName;

public sealed class GetCustomerByNameQuery : IRequest<Customer>
{
    public string? Name { get; set; }
}
