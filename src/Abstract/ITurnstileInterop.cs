using Microsoft.JSInterop;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Turnstile.Options;
using System;

namespace Soenneker.Blazor.Turnstile.Abstract;

public interface ITurnstileInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the Turnstile script.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
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
    /// <param name="elementId">The ID of the element.</param>
    /// <param name="widgetId">The widget ID.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask CreateObserver(string elementId, string widgetId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Resets the specified Turnstile widget.
    /// </summary>
    /// <param name="widgetId">The widget ID.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask Reset(string widgetId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the specified Turnstile widget.
    /// </summary>
    /// <param name="widgetId">The widget ID.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask Remove(string widgetId, CancellationToken cancellationToken = default);
}