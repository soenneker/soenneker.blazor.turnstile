using Microsoft.JSInterop;
using System.Threading.Tasks;
using Soenneker.Blazor.Turnstile.Abstract;
using System.Threading;
using Soenneker.Blazor.Turnstile.Options;
using Soenneker.Utils.Json;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;

namespace Soenneker.Blazor.Turnstile;

///<inheritdoc cref="ITurnstileInterop"/>
public sealed class TurnstileInterop : ITurnstileInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;

    private readonly AsyncInitializer _scriptInitializer;

    private const string _module = "Soenneker.Blazor.Turnstile/js/turnstileinterop.js";
    private const string _moduleName = "TurnstileInterop";

    public TurnstileInterop(IJSRuntime jsRuntime, IResourceLoader resourceLoader)
    {
        _jsRuntime = jsRuntime;
        _resourceLoader = resourceLoader;
        _scriptInitializer = new AsyncInitializer(InitializeScript);
    }

    private async ValueTask InitializeScript(CancellationToken token)
    {
        await _resourceLoader.LoadScriptAndWaitForVariable("https://challenges.cloudflare.com/turnstile/v0/api.js?render=explicit", "turnstile", cancellationToken: token);

        await _resourceLoader.ImportModuleAndWaitUntilAvailable(_module, _moduleName, 100, token);
    }

    public ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        return _scriptInitializer.Init(cancellationToken);
    }

    public async ValueTask<string> Create(DotNetObjectReference<Turnstile> dotnetObj, string elementId, TurnstileOptions options, InternalTurnstileOptions internalOptions,
        CancellationToken cancellationToken = default)
    {
        await _scriptInitializer.Init(cancellationToken);

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
        await _resourceLoader.DisposeModule(_module);

        await _scriptInitializer.DisposeAsync();
    }
}
