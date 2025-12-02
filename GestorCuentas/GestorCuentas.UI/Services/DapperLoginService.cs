using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GestorCuentas.UI.Services;

public class DapperLoginService(IConfiguration configuration, ILogger<DapperLoginService> logger) : ILoginService
{
    private readonly string? _connectionString = configuration.GetConnectionString("GestorCuentasDb");
    private readonly ILogger<DapperLoginService> _logger = logger;

    public async Task<bool> ValidateCredentialsAsync(string idUsuario, string clave, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            _logger.LogError("No se encontró la cadena de conexión 'GestorCuentasDb'. Verifica la configuración de despliegue.");
            return false;
        }

        const string storedProcedure = "PA_ValidarCredenciales";

        try
        {
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al validar credenciales en {StoredProcedure}", storedProcedure);
            return false;
        }
    }
}
