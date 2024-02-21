using Jwt.Project.Domain.Entities;

namespace Jwt.Project.Domain.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task CreateCustomer(Customer customer);
    Task<IEnumerable<Customer>> GetAllCustomer();
    Task<Customer> GetByLogin(string name);
    Task UpdateCustomerPassword(Customer customer);
}
