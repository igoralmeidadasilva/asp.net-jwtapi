using Jwt.Project.Domain.Entities;
using Jwt.Project.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jwt.Project.Application.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Unit>
{
    private ICustomerRepository _repo;
    private ILogger<CreateCustomerCommandHandler> _logger;

    public CreateCustomerCommandHandler(ICustomerRepository repo, ILogger<CreateCustomerCommandHandler> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start Creating Customer {TimeStamp}.", DateTime.Now);
        var customer = new Customer(request.Name, request.Login, request.Password);
        await _repo.CreateCustomer(customer);
        _logger.LogInformation("End Creating Customer {TimeStamp}.", DateTime.Now);
        return Unit.Value;
    }

}
