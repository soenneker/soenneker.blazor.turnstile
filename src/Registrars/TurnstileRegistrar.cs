using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Turnstile.Abstract;

namespace Soenneker.Blazor.Turnstile.Registrars;

/// <summary>
/// A Blazor interop library for Cloudflare Turnstile
/// </summary>
public static class TurnstileRegistrar
{
    /// <summary>
    /// Adds <see cref="ITurnstile"/> as a scoped service. <para/>
    /// </summary>
    public static void AddTurnstile(this IServiceCollection services)
    {
        services.TryAddScoped<ITurnstileInterop, TurnstileInterop>();
    }
}
