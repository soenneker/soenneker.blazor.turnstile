[![](https://img.shields.io/nuget/v/soenneker.blazor.turnstile.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.turnstile/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.turnstile/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.turnstile/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.turnstile.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.turnstile/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.turnstile/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.turnstile/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Turnstile

Renders a Cloudflare Turnstile widget in Blazor and exposes its token, expiration, error, reset, and removal lifecycle.

## Installation

```bash
dotnet add package Soenneker.Blazor.Turnstile
```

## Registration

```csharp
using Soenneker.Blazor.Turnstile.Registrars;

builder.Services.AddTurnstileInteropAsScoped();
```

## Render a widget

```razor
@using Soenneker.Blazor.Turnstile
@using Soenneker.Blazor.Turnstile.Options

<Turnstile Options="_options"
           OnCallback="HandleToken"
           OnExpiredCallback="HandleExpiration"
           OnErrorCallback="HandleError" />

@code {
    private readonly TurnstileOptions _options = new()
    {
        SiteKey = "your-site-key",
        Action = "sign-in"
    };

    private Task HandleToken(string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            // Send the token to a trusted server for Siteverify validation.
        }

        return Task.CompletedTask;
    }

    private Task HandleExpiration(string _) => Task.CompletedTask;
    private Task HandleError(string message) => Task.CompletedTask;
}
```

`OnCallback` and `TokenChanged` are raised when a token is issued and again with a cleared value after expiration or an error. `OnExpiredCallback` and `OnErrorCallback` provide the corresponding reason. The component's `Token` and `WidgetId` properties expose the current values.

The browser token is not proof that a request is legitimate. Send it to a trusted server and validate it with Cloudflare's Siteverify API before performing the protected operation. Keep the Turnstile secret on the server; never include it in Blazor WebAssembly code or browser configuration.

## Manual lifecycle

Set `ManualCreate = true` when application code needs to control widget creation. After the component has rendered, call `Create()` and retain its returned widget ID. With automatic creation, use `Reset()` to request another challenge and `Remove()` to remove the JavaScript widget.

```razor
<Turnstile @ref="_turnstile" Options="_manualOptions" />

@code {
    private Turnstile? _turnstile;

    private readonly TurnstileOptions _manualOptions = new()
    {
        SiteKey = "your-site-key",
        ManualCreate = true
    };

    private async Task CreateWidget()
    {
        if (_turnstile is not null)
            await _turnstile.Create();
    }
}
```

Calling `Create()` before the component's first render throws `InvalidOperationException`. Calling it again without first removing the widget returns `null`. The component disposes its .NET reference when it leaves the render tree; the scoped interop service is owned by dependency injection.

See Cloudflare's [client-side rendering documentation](https://developers.cloudflare.com/turnstile/get-started/client-side-rendering/) for supported widget settings and the [Siteverify documentation](https://developers.cloudflare.com/turnstile/get-started/server-side-validation/) for server validation.
