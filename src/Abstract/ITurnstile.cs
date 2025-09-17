using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Turnstile.Options;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Quark.Components.Cancellable.Abstract;

namespace Soenneker.Blazor.Turnstile.Abstract;

/// <summary>
/// A Blazor interop library for Cloudflare Turnstile
/// </summary>
public interface ITurnstile : ICancellableComponent
{
    /// <summary>
    /// Gets or sets the configuration options for the Turnstile widget.
    /// </summary>
    TurnstileOptions? Options { get; set; }

    /// <summary>
    /// Additionally available in <see cref="Options"/>, this is a convenience parameter to set the site key directly.
    /// </summary>
    string? SiteKey { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the token changes.
    /// </summary>
    EventCallback<string> TokenChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked upon successful challenge completion, passing a token that can be validated.
    /// </summary>
    EventCallback<string> OnCallback { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the token expires.
    /// </summary>
    EventCallback<string> OnExpiredCallback { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when an error occurs, such as a network error or challenge failure.
    /// </summary>
    EventCallback<string> OnErrorCallback { get; set; }

    /// <summary>
    /// Gets the widget ID.
    /// </summary>
    /// <remarks>
    /// This is returned from Cloudflare JS once created, this is not the element's id used for initialization.
    /// </remarks>
    string? WidgetId { get; }

    /// <summary>
    /// Gets the token that can be validated, populated upon successful challenge in <see cref="OnCallback"/>.
    /// </summary>
    string? Token { get; }

    /// <summary>
    /// Creates the Turnstile widget with the specified options.
    /// </summary>
    /// <param name="options">The configuration options for the widget. If null, default options will be used.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous create operation.</returns>
    ValueTask<string?> Create(TurnstileOptions? options = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resets the Turnstile widget, clearing any current state or input.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous reset operation.</returns>
    ValueTask Reset(CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the Turnstile widget from the DOM.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous remove operation.</returns>
    ValueTask Remove(CancellationToken cancellationToken = default);
}
