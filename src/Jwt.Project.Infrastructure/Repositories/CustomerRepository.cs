using Dapper;
using Jwt.Project.Domain.Entities;
using Jwt.Project.Domain.Interfaces.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Jwt.Project.Infrastructure.Repositories;

public sealed class CustomerRepository : BaseSqliteRepository, ICustomerRepository
{
    private ILogger<CustomerRepository> _logger;

    public CustomerRepository(
        IConfiguration configuration,
        ILogger<CustomerRepository> logger) : base(configuration)
    { 
        _logger = logger;
    }

    public async Task CreateCustomer(Customer customer)
    {
        string sql = @"INSERT INTO Customers (id, name, login, password) VALUES (@Id, @Name, @Login, @Password);";
        try
        {
            using var conn = await OpenConnection();

            var queryObject = new 
            {
                Id = customer.Id.ToString(),
                Name = customer.Name,
                Login = customer.Login,
                Password = customer.Password
            };
            
            var response = await conn.ExecuteAsync(sql, queryObject).ConfigureAwait(false);;
            
            _logger.LogInformation("{rowsAffected} row(s) inserted.", response);
        }
        catch(SqliteException ex)
        {
            _logger.LogError("Error in database access - {CreateCustomer}: {ex}.", nameof(CreateCustomer), ex);
            throw;
        }
    }

    public async Task<Customer> GetByLogin(string login)
    {
        string sql = @"SELECT * FROM Customers WHERE login = @Login";
        try
        {   
            var conn = await OpenConnection();
            var queryObject = new
            {
                Login = login
            };
            var response = (await conn.QueryAsync<Customer>(sql, queryObject).ConfigureAwait(false)).FirstOrDefault();
            await conn.CloseAsync();
            return response!;
        }
        catch(SqliteException ex)
        {
            _logger.LogError("Error in database access - {GetByLogin}: {ex}.", nameof(GetByLogin), ex);
            throw;
        }
    }

    public Task<IEnumerable<Customer>> GetAllCustomer()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCustomerPassword(Customer customer)
    {
        string sql = @"UPDATE Customers
                        SET password = @Password
                        WHERE login = @Login;";
        try
        {
            using var conn = await OpenConnection();
            var queryObject = new
            {
                Login = customer.Login,
                Password = customer.Password
            };
            var response = await conn.ExecuteAsync(sql, queryObject).ConfigureAwait(false);
            _logger.LogInformation("{rowsAffected} row(s) inserted.", response);
        }
        catch(SqliteException ex)
        {
            _logger.LogError("Error in database access - {UpdateCustomerPassword}: {ex}.", nameof(UpdateCustomerPassword), ex);
            throw;
        }
    }

}
