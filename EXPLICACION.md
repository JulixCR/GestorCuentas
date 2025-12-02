# Explicación de la estructura actual

Este documento resume el comportamiento de autenticación y navegación implementado en la interfaz de Blazor.

## Autenticación con cookies

- **Configuración:** `GestorCuentas.UI/Program.cs`
  - Registra autenticación con cookies (esquema "Cookies") y define `/login` como ruta de inicio de sesión y `/denied` como página de acceso denegado.
  - Agrega autorización, el proveedor de estado de autenticación del servidor y `HttpContextAccessor` para poder emitir cookies desde los componentes interactivos.
  - Inserta `UseAuthentication()` y `UseAuthorization()` en el pipeline de la app antes del mapeo de componentes.

## Flujo de inicio de sesión

- **Componente:** `GestorCuentas.UI/Components/Pages/Home.razor`
  - Rutas `/` y `/login` marcadas con `[AllowAnonymous]` para mostrar el formulario.
  - Valida las credenciales con `LoginService.ValidateCredentialsAsync`.
  - Si las credenciales son válidas, crea un `ClaimsPrincipal`, firma con `SignInAsync` usando cookies persistentes (con expiración definida) y redirige al Dashboard.
  - Si son inválidas, muestra un mensaje de error y asegura el cierre de sesión con `SignOutAsync`.

## Protección de páginas

- **Páginas protegidas:**
  - `Dashboard.razor`, `Ventas.razor` y `Gastos.razor` están anotadas con `[Authorize]`, por lo que solo son accesibles para usuarios autenticados.
  - `Components/Routes.razor` utiliza `AuthorizeRouteView` para mostrar un mensaje de "No autorizado" y enlace al login cuando un visitante no está autenticado.
  - `Components/Pages/Denied.razor` muestra el mensaje de acceso denegado configurado en el middleware de cookies.

## Barra de navegación condicionada

- **Componente:** `GestorCuentas.UI/Components/Layout/MainLayout.razor`
  - Muestra la barra de navegación solo dentro de `<AuthorizeView>` para que únicamente aparezca cuando el usuario tiene una identidad autenticada.
  - Incluye enlaces a **Inicio**, **Ventas** y **Gastos**, además del botón de **Cerrar sesión** que invoca `SignOutAsync` y regresa al formulario de login.

## Estilos aplicados

- **Archivo:** `GestorCuentas.UI/Components/Layout/MainLayout.razor.css`
  - Agrega relleno al contenedor principal y estilos para el banner de error de Blazor.

En conjunto, el usuario inicia sesión en `/login` mediante cookies, accede a Dashboard y al resto de secciones protegidas, y solo ve la barra de navegación mientras su cookie de autenticación esté vigente.
