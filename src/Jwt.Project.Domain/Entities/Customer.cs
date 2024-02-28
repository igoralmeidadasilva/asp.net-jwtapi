using System.ComponentModel.DataAnnotations.Schema;
using Jwt.Project.Domain.Enums;

namespace Jwt.Project.Domain.Entities;

[Table("Customers")]
public sealed class Customer
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty; 
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public CustomerRole Role { get; set; }
    
    public Customer(string name, string login, string password, CustomerRole role)
    {
        Id = Guid.NewGuid();
        Name = name;
        Login = login;
        Password = password;
        Role = role;
    }

    public Customer(string id, string name, string login, string password, string role)
    {
        Id = new Guid(id);
        Name = name;
        Login = login;
        Password = password;
        
        if(Enum.TryParse(role, out CustomerRole userRole))
        {
            Role = userRole;
        }
        else
        {
            throw new ArgumentException("Papel de usuário inválido.", nameof(role));
        }
    }
    
}
