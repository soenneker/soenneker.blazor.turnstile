using Ardalis.SmartEnum;
using Soenneker.Extensions.String;

namespace Soenneker.Blazor.Turnstile.Enums;

/// <summary>
/// Represents the options for automatically refreshing the token when it expires.
/// </summary>
public sealed class TurnstileRefreshExpired : SmartEnum<TurnstileRefreshExpired>
{
    /// <summary>
    /// Automatically refreshes the token when it expires.
    /// </summary>
    public static readonly TurnstileRefreshExpired Auto = new(nameof(Auto).ToLowerInvariantFast(), 0);

    /// <summary>
    /// Requires manual intervention to refresh the token when it expires.
    /// </summary>
    public static readonly TurnstileRefreshExpired Manual = new(nameof(Manual).ToLowerInvariantFast(), 1);

    /// <summary>
    /// Never refreshes the token when it expires.
    /// </summary>
    public static readonly TurnstileRefreshExpired Never = new(nameof(Never).ToLowerInvariantFast(), 2);

    private TurnstileRefreshExpired(string name, int value) : base(name, value)
    {
    }
}