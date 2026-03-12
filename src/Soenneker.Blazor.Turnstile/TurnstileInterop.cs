using Microsoft.JSInterop;
using System.Threading.Tasks;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Turnstile.Abstract;
using Soenneker.Blazor.Turnstile.Options;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using Soenneker.Utils.Json;
using System.Threading;

namespace Soenneker.Blazor.Turnstile;

///<inheritdoc cref="ITurnstileInterop"/>
public sealed class TurnstileInterop : ITurnstileInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IResourceLoader _resourceLoader;

    private readonly AsyncInitializer _scriptInitializer;

    private const string _module = "Soenneker.Blazor.Turnstile/js/turnstileinterop.js";
    private const string _moduleName = "TurnstileInterop";

    private readonly CancellationScope _cancellationScope = new();

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

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _scriptInitializer.Init(linked);
    }

    public async ValueTask<string> Create(DotNetObjectReference<Turnstile> dotnetObj, string elementId, TurnstileOptions options, InternalTurnstileOptions internalOptions,
        CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _scriptInitializer.Init(linked);

            string optionsJson = JsonUtil.Serialize(options)!;
            string internalOptionsJson = JsonUtil.Serialize(internalOptions)!;

            return await _jsRuntime.InvokeAsync<string>("TurnstileInterop.create", linked, elementId, optionsJson, internalOptionsJson, dotnetObj);
        }
    }

    public async ValueTask CreateObserver(string elementId, string widgetId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _jsRuntime.InvokeVoidAsync("TurnstileInterop.createObserver", linked, elementId, widgetId);
    }

    public async ValueTask Reset(string widgetId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _jsRuntime.InvokeVoidAsync("turnstile.reset", linked, widgetId);
    }

    public async ValueTask Remove(string widgetId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _jsRuntime.InvokeVoidAsync("turnstile.remove", linked, widgetId);
    }

    public async ValueTask DisposeAsync()
    {
        await _resourceLoader.DisposeModule(_module);

        await _scriptInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
