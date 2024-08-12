
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FileUploadLibrary;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _configuration;

    public SqlDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<T>> LoadData<T>(string storeProc, string connectionName, object? parameter)
    {
        string connectionstring = _configuration.GetConnectionString(connectionName)
                    ?? throw new Exception($"Missing connection string at {connectionName}");

        using var connection = new SqlConnection(connectionstring);

        var row = await connection.QueryAsync<T>(storeProc, parameter, commandType: CommandType.StoredProcedure);

        return row.ToList();
    }
    public async Task SaveData(string storeProc, string connectionName, object parameter)
    {
        string connectionstring = _configuration.GetConnectionString(connectionName)
            ?? throw new Exception($"Missing connection string at {connectionName}");

        using var connection = new SqlConnection(connectionstring);
        await connection.ExecuteAsync(storeProc, parameter, commandType: CommandType.StoredProcedure);
    }
}
