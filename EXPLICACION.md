# Explicación de la estructura actual

Este documento resume el comportamiento agregado recientemente en la interfaz de Blazor.

## Layout principal y barra de navegación

- **Componente:** `GestorCuentas.UI/Components/Layout/MainLayout.razor`
- **Qué hace:**
  - Muestra una barra de navegación con enlaces a **Inicio** (`/dashboard`), **Ventas** (`/ventas`) y **Gastos** (`/gastos`).
  - Controla la visibilidad de la barra evaluando la ruta actual: si la URL relativa está vacía (pantalla de login en `/`), la barra se oculta; en cualquier otra página se muestra.
  - Se suscribe al evento `NavigationManager.LocationChanged` para recalcular la visibilidad cuando cambia la ruta y libera la suscripción en `Dispose` para evitar fugas de eventos.

## Pantalla de inicio de sesión

- **Componente:** `GestorCuentas.UI/Components/Pages/Home.razor`
- **Qué hace:**
  - Define la ruta raíz (`/`) y muestra un formulario de login con campos **IdUsuario** y **Clave**.
  - Usa `LoginService.ValidateCredentialsAsync` para validar las credenciales. Mientras envía la solicitud muestra el texto "Validando...".
  - Si la validación es exitosa, navega automáticamente a `/dashboard`; en caso contrario muestra un mensaje de error.

## Nuevas pantallas de navegación

- **Dashboard:** `GestorCuentas.UI/Components/Pages/Dashboard.razor` muestra un título y un texto temporal.
- **Ventas:** `GestorCuentas.UI/Components/Pages/Ventas.razor` indica que la sección está en construcción.
- **Gastos:** `GestorCuentas.UI/Components/Pages/Gastos.razor` también está marcada como sección en construcción.

## Estilos aplicados

- **Archivo:** `GestorCuentas.UI/Components/Layout/MainLayout.razor.css`
- **Qué contiene:** agrega relleno al contenedor principal y estilos para el banner de error de Blazor.

En conjunto, estos cambios presentan una experiencia donde el usuario inicia sesión en `/`, tras autenticarse pasa al Dashboard y, a partir de ese punto, ve la barra de navegación para moverse entre las secciones disponibles.
