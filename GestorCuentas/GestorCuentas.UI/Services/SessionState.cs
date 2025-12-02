namespace GestorCuentas.UI.Services;

/// <summary>
/// Gestiona el estado de autenticación del usuario durante la sesión actual.
/// </summary>
public class SessionState
{
    private bool _isAuthenticated;

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

    public void SignIn()
    {
        IsAuthenticated = true;
    }

    public void SignOut()
    {
        IsAuthenticated = false;
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
