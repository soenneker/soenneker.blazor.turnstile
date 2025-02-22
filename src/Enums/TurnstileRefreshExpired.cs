﻿using Intellenum;

namespace Soenneker.Blazor.Turnstile.Enums;

/// <summary>
/// Represents the options for automatically refreshing the token when it expires.
/// </summary>
[Intellenum<string>]
public partial class TurnstileRefreshExpired
{
    /// <summary>
    /// Automatically refreshes the token when it expires.
    /// </summary>
    public static readonly TurnstileRefreshExpired Auto = new("auto");

    /// <summary>
    /// Requires manual intervention to refresh the token when it expires.
    /// </summary>
    public static readonly TurnstileRefreshExpired Manual = new("manual");

    /// <summary>
    /// Never refreshes the token when it expires.
    /// </summary>
    public static readonly TurnstileRefreshExpired Never = new("never");
}