using Microsoft.JSInterop;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Turnstile.Options;

namespace Soenneker.Blazor.Turnstile.Abstract;

internal interface ITurnstileInterop
{
    ValueTask<string> Create(DotNetObjectReference<Turnstile> dotnetObj, string elementId, TurnstileOptions options, InternalTurnstileOptions internalOptions, CancellationToken cancellationToken = default);

    ValueTask Reset(string widgetId, CancellationToken cancellationToken = default);

    ValueTask Remove(string widgetId, CancellationToken cancellationToken = default);
}