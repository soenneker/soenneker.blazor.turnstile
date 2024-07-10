using Microsoft.JSInterop;
using System.Threading.Tasks;
using Soenneker.Blazor.Turnstile.Abstract;
using System.Threading;
using Soenneker.Blazor.Turnstile.Options;
using Soenneker.Utils.Json;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Extensions.ValueTask;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;

namespace Soenneker.Blazor.Turnstile;

///<inheritdoc cref="ITurnstileInterop"/>
internal class TurnstileInterop : ITurnstileInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;

    private readonly AsyncSingleton<object> _scriptInitializer;

    public TurnstileInterop(IJSRuntime jsRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jsRuntime;
        _resourceLoader = resourceLoader;

        _scriptInitializer = new AsyncSingleton<object>(async (token, _) =>
        {
            await _resourceLoader.ImportModuleAndWaitUntilAvailable("Soenneker.Blazor.Turnstile/turnstileinterop.js", "TurnstileInterop", 100, token).NoSync();
            await _resourceLoader.LoadScriptAndWaitForVariable("https://challenges.cloudflare.com/turnstile/v0/api.js?render=explicit", "turnstile", cancellationToken: token).NoSync();
            return new object();
        });
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        _ = await _scriptInitializer.Get(cancellationToken).NoSync();
    }

    public async ValueTask<string> Create(DotNetObjectReference<Turnstile> dotnetObj, string elementId, TurnstileOptions options, InternalTurnstileOptions internalOptions,
        CancellationToken cancellationToken = default)
    {
        _ = await _scriptInitializer.Get(cancellationToken).NoSync();

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

    public ValueTask DisposeAsync()
    {
        return _resourceLoader.DisposeModule("Soenneker.Blazor.Turnstile/turnstileinterop.js");
    }
}