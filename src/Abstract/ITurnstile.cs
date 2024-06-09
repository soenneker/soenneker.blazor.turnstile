using Soenneker.Blazor.Turnstile.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Turnstile.Abstract;

/// <summary>
/// A Blazor interop library for Cloudflare Turnstile
/// </summary>
public interface ITurnstile
{
    /// <summary>
    /// Creates the Turnstile widget with the specified options.
    /// </summary>
    /// <param name="options">The configuration options for the widget. If null, default options will be used.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous create operation.</returns>
    ValueTask Create(TurnstileOptions? options = null, CancellationToken cancellationToken = default);

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