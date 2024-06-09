[![](https://img.shields.io/nuget/v/soenneker.blazor.turnstile.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.turnstile/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.turnstile/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.turnstile/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.turnstile.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.turnstile/)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Blazor.Turnstile
### A Blazor interop library for Cloudflare Turnstile captchas

This library simplifies the integration of Cloudflare Turnstile into Blazor applications, providing access to options, methods, and events. A demo project showcasing common usages is included.

Please refer to the [Cloudflare documentation](https://developers.cloudflare.com/turnstile/get-started/client-side-rendering/) for further details.


## Installation

```
dotnet add package Soenneker.Blazor.Turnstile
```


### 1. Add the following to your `_Imports.razor` file

```razor
@using Soenneker.Blazor.Turnstile
```

### 2. Add the following to your `Startup.cs` file

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddTurnstile();
}
```

### 3. Add a script in the `<head>` in the `wwwroot/index.html` file

```html
<script src="https://challenges.cloudflare.com/turnstile/v0/api.js" defer></script>
```

### 4. Add a script at the bottom of your `<body>` in the `wwwroot/index.html` file

```html
<script src="_content/Soenneker.Blazor.Turnstile/turnstile.js"></script>
```

## Usage

```razor
<Turnstile @ref="_turnstile" OnCallback="OnCallback" Options="_options" ></Turnstile>

@code{
    private readonly TurnstileOptions _options = new()
    {
        SiteKey = "1x00000000000000000000AA" // Testing key
    };

    private void OnCallback(string token)
    {
        // Send this token to your server for validation
        Logger.LogInformation("OnCallback fired, token: {token}", token);
    }
}
```

⚠️ While 95%+ of the Cloudflare Turnstile JS has been implemented, there are a few features not yet supported. If you need assistance or want to request a new feature, please open an issue or submit a pull request.