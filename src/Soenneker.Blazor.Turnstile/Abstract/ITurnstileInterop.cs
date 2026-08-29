using Microsoft.JSInterop;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Turnstile.Options;
using System;

namespace Soenneker.Blazor.Turnstile.Abstract;

/// <summary>
/// Defines the turnstile interop contract.
/// </summary>
public interface ITurnstileInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the Turnstile script.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that completes when the turnstile is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a Turnstile instance.
    /// </summary>
    /// <param name="dotnetObj">A reference to the .NET object.</param>
    /// <param name="elementId">The ID of the element.</param>
    /// <param name="options">The Turnstile options.</param>
    /// <param name="internalOptions">The internal Turnstile options.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The widget ID.</returns>
    ValueTask<string> Create(DotNetObjectReference<Turnstile> dotnetObj, string elementId, TurnstileOptions options, InternalTurnstileOptions internalOptions, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates an observer for a Turnstile widget.
    /// </summary>
    /// <param name="elementId">ID of the DOM element to target.</param>
    /// <param name="widgetId">Identifier of the widget to target.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that completes when the observer creation is complete.</returns>
    ValueTask CreateObserver(string elementId, string widgetId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resets the specified Turnstile widget.
    /// </summary>
    /// <param name="widgetId">Identifier of the widget to target.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that completes when the reset operation is complete.</returns>
    ValueTask Reset(string widgetId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the specified Turnstile widget.
    /// </summary>
    /// <param name="widgetId">Identifier of the widget to target.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that completes when the remove operation is complete.</returns>
    ValueTask Remove(string widgetId, CancellationToken cancellationToken = default);
}
