using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Turnstile.Options;

internal class InternalTurnstileOptions
{
    [JsonPropertyName("callbackEnabled")]
    internal bool? CallbackEventEnabled { get; set; }

    [JsonPropertyName("onErrorEnabled")]
    internal bool? OnErrorEventEnabled { get; set; }

    [JsonPropertyName("onExpiredEnabled")]
    internal bool? OnExpiredEventEnabled { get; set; }
}