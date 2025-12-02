using GestorCuentas.UI.Services.Models;

namespace GestorCuentas.UI.Services;

public interface IAuthService
{
    Task<LoginResult> ValidateCredentialsAsync(string userName, string password, CancellationToken cancellationToken = default);
}
