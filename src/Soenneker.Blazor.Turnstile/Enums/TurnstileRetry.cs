using Soenneker.Gen.EnumValues;

namespace Soenneker.Blazor.Turnstile.Enums;

/// <summary>
/// Represents the retry options for Turnstile.
/// </summary>
[EnumValue<string>]
public sealed partial class TurnstileRetry
{
    /// <summary>
    /// Represents the automatic retry option.
    /// </summary>
    public static readonly TurnstileRetry Auto = new("auto");

    /// <summary>
    /// Represents the option to never retry.
    /// </summary>
    public static readonly TurnstileRetry Never = new("never");
}
