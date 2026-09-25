using Soenneker.Gen.EnumValues;

namespace Soenneker.Blazor.Turnstile.Enums;

/// <summary>
/// The Turnstile widget can have two different sizes when using the Managed or Non-interactive modes:
/// 
/// Size    Width   Height
/// Normal  300px   65px
/// Compact 130px   120px
/// </summary>
[EnumValue<string>]
public sealed partial class TurnstileSize
{
    // Prevent JSON source generation from assuming an implicit public constructor.
    private TurnstileSize() => throw new System.NotSupportedException("Use a declared enum value.");

    /// <summary>
    /// Normal size: Width 300px, Height 65px.
    /// </summary>
    public static readonly TurnstileSize Normal = new("normal");

    /// <summary>
    /// Compact size: Width 130px, Height 120px.
    /// </summary>
    public static readonly TurnstileSize Compact = new("compact");
}
