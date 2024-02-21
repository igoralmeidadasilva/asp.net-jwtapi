using Jwt.Project.Domain.Entities;
using Jwt.Project.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Toolkit.Services;

namespace Jwt.Project.Application.Commands.UpdateCustomer;

public sealed class UpdateCustomerPasswordCommandHandler : IRequestHandler<UpdateCustomerPasswordCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerPasswordCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(UpdateCustomerPasswordCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByLogin(request.Login) ?? throw new UserNotFoundException("Customer Not Found.");

        customer.Password = request.Password;

        await _customerRepository.UpdateCustomerPassword(customer);
        return Unit.Value;
    }
}
