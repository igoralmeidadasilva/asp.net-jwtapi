using System.Data;
using System.Data.SQLite;
using Jwt.Project.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace Jwt.Project.Infrastructure.Repositories;

public abstract class BaseSqliteRepository : IBaseSqliteRepository
{
    private readonly IConfiguration _configuration;
    private readonly SQLiteConnection? _connection;

    protected BaseSqliteRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        string batabasePath = _configuration.GetConnectionString("SQLite")!;
        _connection = new SQLiteConnection(batabasePath);
    }

    protected async Task<SQLiteConnection> OpenConnection()
    {
        if (_connection!.State != ConnectionState.Open)
        {
            await _connection.OpenAsync();
        }
        return _connection;
    }

}
