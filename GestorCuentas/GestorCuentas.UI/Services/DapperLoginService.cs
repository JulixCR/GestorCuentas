using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GestorCuentas.UI.Services;

public class DapperLoginService(IConfiguration configuration) : ILoginService
{
    private readonly string _connectionString = configuration.GetConnectionString("GestorCuentasDb")
        ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'GestorCuentasDb'.");

    public async Task<bool> ValidateCredentialsAsync(string idUsuario, string clave, CancellationToken cancellationToken = default)
    {
        const string storedProcedure = "PA_ValidarCredenciales";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        var result = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                storedProcedure,
                new { IdUsuario = idUsuario, Clave = clave },
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken));

        return result > 0;
    }
}
