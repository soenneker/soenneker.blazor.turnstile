using Ardalis.SmartEnum;
using Soenneker.Extensions.String;

namespace Soenneker.Blazor.Turnstile.Enums;

/// <summary>
/// Represents the retry options for Turnstile.
/// </summary>
public sealed class TurnstileRetry : SmartEnum<TurnstileRetry>
{
    /// <summary>
    /// Represents the automatic retry option.
    /// </summary>
    public static readonly TurnstileRetry Auto = new(nameof(Auto).ToLowerInvariantFast(), 0);

    /// <summary>
    /// Represents the option to never retry.
    /// </summary>
    public static readonly TurnstileRetry Never = new(nameof(Never).ToLowerInvariantFast(), 1);

    private TurnstileRetry(string name, int value) : base(name, value)
    {
    }
}