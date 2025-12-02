using System.ComponentModel.DataAnnotations;

namespace GestorCuentas.UI.Models;

public class LoginModel
{
    [Required(ErrorMessage = "El IdUsuario es obligatorio.")]
    public string? IdUsuario { get; set; }

    [Required(ErrorMessage = "La clave es obligatoria.")]
    public string? Clave { get; set; }
}
