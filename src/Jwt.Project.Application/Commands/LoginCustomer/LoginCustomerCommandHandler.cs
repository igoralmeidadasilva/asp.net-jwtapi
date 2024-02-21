using System.Security.Authentication;
using Jwt.Project.Domain.Interfaces.Repositories;
using Jwt.Project.Domain.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Services;

namespace Jwt.Project.Application.Commands.LoginCustomer;

public sealed class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, string>
{
    private readonly ITokenService _tokenService;
    private readonly ICustomerRepository _customerRepository;

    public LoginCustomerCommandHandler(ITokenService tokenService, ICustomerRepository customerRepository)
    {
        _tokenService = tokenService;
        _customerRepository = customerRepository;
    }

    public async Task<string> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByLogin(request.Login) ?? throw new UserNotFoundException("Customer Not Found.");
        
        if(!request.Password.Equals(customer.Password))
        {
            throw new AuthenticationException("Invalid Password.");
        }

        var response = _tokenService.GenerateToken(customer);
        return response;
    }

}
