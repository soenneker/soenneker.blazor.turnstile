using Microsoft.JSInterop;
using System.Threading.Tasks;
using Soenneker.Blazor.Turnstile.Abstract;
using System.Threading;
using Soenneker.Blazor.Turnstile.Options;
using Soenneker.Utils.Json;

namespace Soenneker.Blazor.Turnstile;

///<inheritdoc cref="ITurnstileInterop"/>
internal class TurnstileInterop : ITurnstileInterop
{
    private readonly IJSRuntime _jsRuntime;

    public TurnstileInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async ValueTask<string> Create(DotNetObjectReference<Turnstile> dotnetObj, string elementId, TurnstileOptions options, InternalTurnstileOptions internalOptions,
        CancellationToken cancellationToken = default)
    {
        string optionsJson = JsonUtil.Serialize(options)!;
        string internalOptionsJson = JsonUtil.Serialize(options)!;

        return await _jsRuntime.InvokeAsync<string>("turnstileinterop.create", cancellationToken, elementId, optionsJson, internalOptionsJson, dotnetObj);
    }

    public ValueTask Reset(string widgetId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync("turnstile.reset", cancellationToken, widgetId);
    }

    public ValueTask Remove(string widgetId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync("turnstile.remove", cancellationToken, widgetId);
    }
}