using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace GestorCuentas.UI.Services;

/// <summary>
/// Gestiona el estado de autenticación del usuario durante la sesión actual.
/// Persiste el estado en el almacenamiento de sesión del navegador para
/// mantenerlo al recargar la página o reconectar la sesión interactiva.
/// </summary>
public class SessionState
{
    private const string AuthKey = "SessionState.IsAuthenticated";

    private readonly ProtectedSessionStorage _sessionStorage;
    private bool _isAuthenticated;
    private bool _initialized;

    public SessionState(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public bool IsAuthenticated
    {
        get => _isAuthenticated;
        private set
        {
            if (_isAuthenticated == value)
            {
                return;
            }

            _isAuthenticated = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    /// <summary>
    /// Asegura que el estado inicial se cargue desde el almacenamiento de sesión.
    /// </summary>
    public async Task EnsureInitializedAsync()
    {
        if (_initialized)
        {
            return;
        }

        var storedResult = await _sessionStorage.GetAsync<bool>(AuthKey);

        if (storedResult.Success)
        {
            _isAuthenticated = storedResult.Value;
        }

        _initialized = true;
    }

    public async Task SignInAsync()
    {
        await EnsureInitializedAsync();

        IsAuthenticated = true;
        await _sessionStorage.SetAsync(AuthKey, true);
    }

    public async Task SignOutAsync()
    {
        await EnsureInitializedAsync();

        IsAuthenticated = false;
        await _sessionStorage.SetAsync(AuthKey, false);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
