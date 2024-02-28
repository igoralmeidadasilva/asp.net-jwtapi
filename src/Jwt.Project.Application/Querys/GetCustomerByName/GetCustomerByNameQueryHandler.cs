using Jwt.Project.Domain.Entities;
using Jwt.Project.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Jwt.Project.Application.Querys.GetCustomerByName
{
    public sealed class GetCustomerByNameQueryHandler : IRequestHandler<GetCustomerByNameQuery, Customer>
    {
        private ICustomerRepository _repo;
        private ILogger<GetCustomerByNameQueryHandler> _logger;

        public GetCustomerByNameQueryHandler(ICustomerRepository repo, ILogger<GetCustomerByNameQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Customer> Handle(GetCustomerByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start Get Customer By Id {TimeStamp}.", DateTime.Now);
            var response = await _repo.GetByLogin(request.Name!);
            _logger.LogInformation("End Creating Customer {TimeStamp}.", DateTime.Now);
            return response;
        }
    }
}
