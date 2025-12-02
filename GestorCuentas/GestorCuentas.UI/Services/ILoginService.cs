namespace GestorCuentas.UI.Services;

public interface ILoginService
{
    Task<bool> ValidateCredentialsAsync(string idUsuario, string clave, CancellationToken cancellationToken = default);
}
