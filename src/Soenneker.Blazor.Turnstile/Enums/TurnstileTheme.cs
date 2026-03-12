using Soenneker.Gen.EnumValues;

namespace Soenneker.Blazor.Turnstile.Enums;

/// <summary>
/// Represents the theme options for Turnstile.
/// </summary>
[EnumValue<string>]
public sealed partial class TurnstileTheme
{
    /// <summary>
    /// Represents the Light theme.
    /// </summary>
    public static readonly TurnstileTheme Light = new("light");

    /// <summary>
    /// Represents the Dark theme.
    /// </summary>
    public static readonly TurnstileTheme Dark = new("dark");

    /// <summary>
    /// Represents the Auto theme, which adjusts automatically based on the system settings.
    /// </summary>
    public static readonly TurnstileTheme Auto = new("auto");
}
