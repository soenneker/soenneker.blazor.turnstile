using Ardalis.SmartEnum;
using Soenneker.Extensions.String;

namespace Soenneker.Blazor.Turnstile.Enums;

/// <summary>
/// Execution modes
/// 
/// By default, Turnstile tokens are obtained for a visitor upon the rendering of a widget (even in invisible mode).
/// However, in some scenarios, an application may want to embed Turnstile, but defer running the challenge until a
/// certain point in time. This is where execution mode can be used to control when a challenge runs and a token is
/// being generated.
/// 
/// There are two options:
/// 
/// 1. The challenge runs automatically after calling the Render() function.
/// 2. The challenge runs after the Render() function has been called, by invoking the turnstile.Execute(container: string | HTMLElement, jsParams?: RenderParameters) function separately.
///    This detaches the appearance and rendering of a widget from its execution.
/// </summary>
public sealed class TurnstileExecution : SmartEnum<TurnstileExecution>
{
    /// <summary>
    /// The challenge runs automatically after calling the Render() function.
    /// </summary>
    public static readonly TurnstileExecution Render = new(nameof(Render).ToLowerInvariantFast(), 0);

    /// <summary>
    /// The challenge runs after the Render() function has been called, by invoking the turnstile.Execute(container: string | HTMLElement, jsParams?: RenderParameters) function separately.
    /// This detaches the appearance and rendering of a widget from its execution.
    /// </summary>
    public static readonly TurnstileExecution Execute = new(nameof(Execute).ToLowerInvariantFast(), 1);

    private TurnstileExecution(string name, int value) : base(name, value)
    {
    }
}