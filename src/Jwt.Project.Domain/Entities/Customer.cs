using System.ComponentModel.DataAnnotations.Schema;

namespace Jwt.Project.Domain.Entities;

[Table("Customers")]
public sealed class Customer
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty; 
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public Customer(string id, string name, string login, string password)
    {
        Id = new Guid(id);
        Name = name;
        Login = login;
        Password = password;
    }

    public Customer(string name, string login, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Login = login;
        Password = password;
    }
    
}
