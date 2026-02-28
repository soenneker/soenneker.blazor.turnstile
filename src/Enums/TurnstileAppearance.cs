using Soenneker.Gen.EnumValues;

namespace Soenneker.Blazor.Turnstile.Enums;

/// <summary>
/// If a widget is visible, its appearance can be controlled via the appearance parameter.
/// 
/// By default, appearance is set to Always for visible widget types. However, if appearance is set to Execute,
/// the widget will only become visible after the challenge begins. This is helpful in situations where Execute()
/// is called after Render(). If appearance is set to interaction-only, the widget will become only visible in
/// cases where an interaction is required.
/// </summary>
[EnumValue<string>]
public sealed partial class TurnstileAppearance
{
    /// <summary>
    /// The widget is always visible.
    /// </summary>
    public static readonly TurnstileAppearance Always = new("always");

    /// <summary>
    /// The widget will only become visible after the challenge begins.
    /// This is helpful in situations where Execute() is called after Render().
    /// </summary>
    public static readonly TurnstileAppearance Execute = new("execute");

    /// <summary>
    /// The widget will become visible only in cases where an interaction is required.
    /// </summary>
    public static readonly TurnstileAppearance InteractionOnly = new("interaction-only");
}
