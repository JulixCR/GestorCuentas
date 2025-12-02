using System.Data;
using Dapper;
using GestorCuentas.UI.Services.Models;
using Microsoft.Data.SqlClient;

namespace GestorCuentas.UI.Services;

public class AuthService : IAuthService
{
    private readonly string _connectionString;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IConfiguration configuration, ILogger<AuthService> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'.");
        _logger = logger;
    }

    public async Task<LoginResult> ValidateCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
        {
            return LoginResult.Failure("Usuario y contraseña son obligatorios.");
        }

        const string query = @"SELECT TOP 1 Id, UserName, Password FROM Usuarios WHERE UserName = @UserName";

        try
        {
            await using var connection = new SqlConnection(_connectionString);
            var user = await connection.QuerySingleOrDefaultAsync<UserRecord>(
                new CommandDefinition(query, new { UserName = userName }, cancellationToken: cancellationToken));

            if (user is null)
            {
                return LoginResult.Failure("Usuario no encontrado.");
            }

            if (!string.Equals(user.Password, password, StringComparison.Ordinal))
            {
                return LoginResult.Failure("Contraseña incorrecta.");
            }

            return LoginResult.Successful(user.UserName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al validar el usuario {UserName}", userName);
            return LoginResult.Failure("No se pudo validar el usuario. Intente nuevamente más tarde.");
        }
    }

    private record UserRecord(int Id, string UserName, string Password);
}
