using Microsoft.JSInterop;
using System.Threading.Tasks;
using Soenneker.Blazor.Turnstile.Abstract;
using System.Threading;
using Soenneker.Blazor.Turnstile.Options;
using Soenneker.Utils.Json;
using System;

namespace Soenneker.Blazor.Turnstile;

///<inheritdoc cref="ITurnstileInterop"/>
internal class TurnstileInterop : ITurnstileInterop
{
    private readonly IJSRuntime _jsRuntime;
    private IJSObjectReference? _scriptRef;

    public TurnstileInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async ValueTask<string> Create(DotNetObjectReference<Turnstile> dotnetObj, string elementId, TurnstileOptions options, InternalTurnstileOptions internalOptions,
        CancellationToken cancellationToken = default)
    {
        _scriptRef = await _jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", cancellationToken, "./_content/Soenneker.Blazor.Turnstile/turnstileinterop.js");

        string optionsJson = JsonUtil.Serialize(options)!;
        string internalOptionsJson = JsonUtil.Serialize(internalOptions)!;

        return await _jsRuntime.InvokeAsync<string>("TurnstileInterop.create", cancellationToken, elementId, optionsJson, internalOptionsJson, dotnetObj);
    }

    public ValueTask CreateObserver(string elementId, string widgetId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync("TurnstileInterop.createObserver", cancellationToken, elementId, widgetId);
    }

    public ValueTask Reset(string widgetId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync("turnstile.reset", cancellationToken, widgetId);
    }

    public ValueTask Remove(string widgetId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsync("turnstile.remove", cancellationToken, widgetId);
    }

    public async ValueTask DisposeAsync()
    {
        if (_scriptRef is not null)
            await _scriptRef.DisposeAsync();
    }
}