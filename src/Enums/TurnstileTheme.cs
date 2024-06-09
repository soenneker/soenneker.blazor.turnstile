using Ardalis.SmartEnum;
using Soenneker.Extensions.String;

namespace Soenneker.Blazor.Turnstile.Enums;

/// <summary>
/// Represents the theme options for Turnstile.
/// </summary>
public sealed class TurnstileTheme : SmartEnum<TurnstileTheme>
{
    /// <summary>
    /// Represents the Light theme.
    /// </summary>
    public static readonly TurnstileTheme Light = new(nameof(Light).ToLowerInvariantFast(), 0);

    /// <summary>
    /// Represents the Dark theme.
    /// </summary>
    public static readonly TurnstileTheme Dark = new(nameof(Dark).ToLowerInvariantFast(), 1);

    /// <summary>
    /// Represents the Auto theme, which adjusts automatically based on the system settings.
    /// </summary>
    public static readonly TurnstileTheme Auto = new(nameof(Auto).ToLowerInvariantFast(), 2);

    private TurnstileTheme(string name, int value) : base(name, value)
    {
    }
}