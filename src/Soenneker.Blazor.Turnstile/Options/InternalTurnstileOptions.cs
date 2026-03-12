using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Turnstile.Options;

public sealed class InternalTurnstileOptions
{
    [JsonPropertyName("callbackEnabled")]
    public bool? CallbackEventEnabled { get; set; }

    [JsonPropertyName("onErrorEnabled")]
    public bool? OnErrorEventEnabled { get; set; }

    [JsonPropertyName("onExpiredEnabled")]
    public bool? OnExpiredEventEnabled { get; set; }
}