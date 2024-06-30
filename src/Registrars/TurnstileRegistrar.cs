using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Turnstile.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Blazor.Turnstile.Registrars;

/// <summary>
/// A Blazor interop library for Cloudflare Turnstile
/// </summary>
public static class TurnstileRegistrar
{
    /// <summary>
    /// Adds <see cref="ITurnstile"/> as a singleton service. <para/>
    /// </summary>
    public static void AddTurnstile(this IServiceCollection services)
    {
        services.AddResourceLoader();
        services.TryAddSingleton<ITurnstileInterop, TurnstileInterop>();
    }
}
