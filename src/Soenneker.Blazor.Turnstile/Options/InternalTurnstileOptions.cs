using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Turnstile.Options;

/// <summary>
/// Represents the internal turnstile options.
/// </summary>
public sealed class InternalTurnstileOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether callback event enabled.
    /// </summary>
    [JsonPropertyName("callbackEnabled")]
    public bool? CallbackEventEnabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether on error event enabled.
    /// </summary>
    [JsonPropertyName("onErrorEnabled")]
    public bool? OnErrorEventEnabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether on expired event enabled.
    /// </summary>
    [JsonPropertyName("onExpiredEnabled")]
    public bool? OnExpiredEventEnabled { get; set; }
}