namespace GestorCuentas.UI.Services.Models;

public record LoginResult(bool Success, string? DisplayName, string? ErrorMessage)
{
    public static LoginResult Successful(string displayName) => new(true, displayName, null);

    public static LoginResult Failure(string errorMessage) => new(false, null, errorMessage);
}
