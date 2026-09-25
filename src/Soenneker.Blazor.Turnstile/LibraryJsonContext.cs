using Soenneker.Blazor.Turnstile.Options;
using System.Text.Json.Serialization.Metadata;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace Soenneker.Blazor.Turnstile;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, ReadCommentHandling = JsonCommentHandling.Skip, UseStringEnumConverter = true, Converters = new[] { typeof(TurnstileAppearanceMetadataConverter), typeof(TurnstileExecutionMetadataConverter), typeof(TurnstileRefreshExpiredMetadataConverter), typeof(TurnstileRetryMetadataConverter), typeof(TurnstileSizeMetadataConverter), typeof(TurnstileThemeMetadataConverter) })]
[JsonSerializable(typeof(InternalTurnstileOptions))]
[JsonSerializable(typeof(TurnstileOptions))]
[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(long))]
[JsonSerializable(typeof(double))]
[JsonSerializable(typeof(decimal))]
[JsonSerializable(typeof(float))]
[JsonSerializable(typeof(System.Text.Json.JsonElement))]
[JsonSerializable(typeof(System.Collections.Generic.Dictionary<string, object?>))]
[JsonSerializable(typeof(System.Collections.Generic.List<object?>))]
[JsonSerializable(typeof(string[]))]
[JsonSerializable(typeof(object[]))]
internal partial class LibraryJsonContext : JsonSerializerContext
{
    internal static JsonTypeInfo<T> Get<T>() =>
        (JsonTypeInfo<T>)(Default.GetTypeInfo(typeof(T)) ?? throw new NotSupportedException($"No generated JSON metadata for {typeof(T)}."));

    internal static JsonSerializerOptions WithContext(JsonSerializerContext? additionalContext)
    {
        JsonSerializerOptions defaults = Get<object>().Options;
        if (additionalContext is null)
            return defaults;
        var options = new JsonSerializerOptions(defaults)
        {
            TypeInfoResolver = JsonTypeInfoResolver.Combine(defaults.TypeInfoResolver!, additionalContext)
        };
        options.MakeReadOnly();
        return options;
    }
}

internal sealed class TurnstileAppearanceMetadataConverter : JsonConverter<Soenneker.Blazor.Turnstile.Enums.TurnstileAppearance>
{
    public override Soenneker.Blazor.Turnstile.Enums.TurnstileAppearance Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.String && Soenneker.Blazor.Turnstile.Enums.TurnstileAppearance.TryFromValue(reader.GetString(), out var value) ? value : throw new JsonException("Unknown TurnstileAppearance value.");

    public override void Write(Utf8JsonWriter writer, Soenneker.Blazor.Turnstile.Enums.TurnstileAppearance value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Value);
}

internal sealed class TurnstileExecutionMetadataConverter : JsonConverter<Soenneker.Blazor.Turnstile.Enums.TurnstileExecution>
{
    public override Soenneker.Blazor.Turnstile.Enums.TurnstileExecution Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.String && Soenneker.Blazor.Turnstile.Enums.TurnstileExecution.TryFromValue(reader.GetString(), out var value) ? value : throw new JsonException("Unknown TurnstileExecution value.");

    public override void Write(Utf8JsonWriter writer, Soenneker.Blazor.Turnstile.Enums.TurnstileExecution value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Value);
}

internal sealed class TurnstileRefreshExpiredMetadataConverter : JsonConverter<Soenneker.Blazor.Turnstile.Enums.TurnstileRefreshExpired>
{
    public override Soenneker.Blazor.Turnstile.Enums.TurnstileRefreshExpired Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.String && Soenneker.Blazor.Turnstile.Enums.TurnstileRefreshExpired.TryFromValue(reader.GetString(), out var value) ? value : throw new JsonException("Unknown TurnstileRefreshExpired value.");

    public override void Write(Utf8JsonWriter writer, Soenneker.Blazor.Turnstile.Enums.TurnstileRefreshExpired value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Value);
}

internal sealed class TurnstileRetryMetadataConverter : JsonConverter<Soenneker.Blazor.Turnstile.Enums.TurnstileRetry>
{
    public override Soenneker.Blazor.Turnstile.Enums.TurnstileRetry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.String && Soenneker.Blazor.Turnstile.Enums.TurnstileRetry.TryFromValue(reader.GetString(), out var value) ? value : throw new JsonException("Unknown TurnstileRetry value.");

    public override void Write(Utf8JsonWriter writer, Soenneker.Blazor.Turnstile.Enums.TurnstileRetry value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Value);
}

internal sealed class TurnstileSizeMetadataConverter : JsonConverter<Soenneker.Blazor.Turnstile.Enums.TurnstileSize>
{
    public override Soenneker.Blazor.Turnstile.Enums.TurnstileSize Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.String && Soenneker.Blazor.Turnstile.Enums.TurnstileSize.TryFromValue(reader.GetString(), out var value) ? value : throw new JsonException("Unknown TurnstileSize value.");

    public override void Write(Utf8JsonWriter writer, Soenneker.Blazor.Turnstile.Enums.TurnstileSize value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Value);
}

internal sealed class TurnstileThemeMetadataConverter : JsonConverter<Soenneker.Blazor.Turnstile.Enums.TurnstileTheme>
{
    public override Soenneker.Blazor.Turnstile.Enums.TurnstileTheme Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.String && Soenneker.Blazor.Turnstile.Enums.TurnstileTheme.TryFromValue(reader.GetString(), out var value) ? value : throw new JsonException("Unknown TurnstileTheme value.");

    public override void Write(Utf8JsonWriter writer, Soenneker.Blazor.Turnstile.Enums.TurnstileTheme value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Value);
}
