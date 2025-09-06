using System.Text.Json.Serialization;
using Soenneker.Blazor.Turnstile.Enums;

namespace Soenneker.Blazor.Turnstile.Options;

/// <summary>
/// Represents the configuration options for the Turnstile widget.
/// </summary>
public sealed class TurnstileOptions
{
    /// <summary>
    /// The site key associated with the widget configuration, created during widget creation.
    /// </summary>
    [JsonPropertyName("sitekey")]
    public string? SiteKey { get; set; }

    /// <summary>
    /// If true, "Create" will not get called automatically from OnAfterRenderAsync. 
    /// This is helpful when Create needs to be called manually from a component, for example.
    /// </summary>
    [JsonIgnore]
    public bool ManualCreate { get; set; }

    /// <summary>
    /// A custom value that can be used to differentiate widgets under the same site key in analytics and which is returned upon validation. 
    /// This can only contain up to 32 alphanumeric characters including underscores (_) and hyphens (-).
    /// </summary>
    [JsonPropertyName("action")]
    public string? Action { get; set; }

    /// <summary>
    /// A custom payload that can be used to attach customer data to the challenge throughout its issuance and which is returned upon validation.
    /// This can only contain up to 255 alphanumeric characters including underscores (_) and hyphens (-).
    /// </summary>
    [JsonPropertyName("cData")]
    public string? CustomerData { get; set; }

    /// <summary>
    /// The language to be used for the widget.
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; set; }

    /// <summary>
    /// The appearance setting for the widget.
    /// </summary>
    [JsonPropertyName("appearance")]
    public TurnstileAppearance? Appearance { get; set; } = TurnstileAppearance.Always;

    /// <summary>
    /// The theme setting for the widget.
    /// </summary>
    [JsonPropertyName("theme")]
    public TurnstileTheme? Theme { get; set; } = TurnstileTheme.Auto;

    /// <summary>
    /// The size setting for the widget.
    /// </summary>
    [JsonPropertyName("size")]
    public TurnstileSize? Size { get; set; } = TurnstileSize.Normal;

    /// <summary>
    /// The execution mode for the widget.
    /// </summary>
    [JsonPropertyName("execution")]
    public TurnstileExecution? Execution { get; set; } = TurnstileExecution.Render;

    /// <summary>
    /// The retry policy for the widget.
    /// </summary>
    [JsonPropertyName("retry")]
    public TurnstileRetry? Retry { get; set; } = TurnstileRetry.Auto;

    /// <summary>
    /// The refresh policy for expired tokens.
    /// </summary>
    [JsonPropertyName("refresh-expired")]
    public TurnstileRefreshExpired? RefreshExpired { get; set; } = TurnstileRefreshExpired.Auto;

    /// <summary>
    /// The interval between retry attempts, in milliseconds.
    /// </summary>
    [JsonPropertyName("retry-interval")]
    public int? RetryInterval { get; set; } = 8000;

    /// <summary>
    /// Indicates whether a response field should be included in the form.
    /// </summary>
    [JsonPropertyName("response_field")]
    public bool? ResponseField { get; set; }

    /// <summary>
    /// The name of the response field.
    /// </summary>
    [JsonPropertyName("response_field_name")]
    public string? ResponseFieldName { get; set; }

    /// <summary>
    /// The tabindex attribute for the widget.
    /// </summary>
    [JsonPropertyName("tabindex")]
    public int? TabIndex { get; set; } = 0;
}
