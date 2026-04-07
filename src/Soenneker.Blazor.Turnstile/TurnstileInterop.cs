using Microsoft.JSInterop;
using System.Threading.Tasks;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Turnstile.Abstract;
using Soenneker.Blazor.Turnstile.Options;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using Soenneker.Utils.Json;
using System.Threading;

namespace Soenneker.Blazor.Turnstile;

///<inheritdoc cref="ITurnstileInterop"/>
public sealed class TurnstileInterop : ITurnstileInterop
{
    private readonly IResourceLoader _resourceLoader;
    private readonly IModuleImportUtil _moduleImportUtil;

    private readonly AsyncInitializer _scriptInitializer;

    private const string _wrapperModulePath = "/_content/Soenneker.Blazor.Turnstile/js/turnstileinterop.js";

    private readonly CancellationScope _cancellationScope = new();

    public TurnstileInterop(IResourceLoader resourceLoader, IModuleImportUtil moduleImportUtil)
    {
        _resourceLoader = resourceLoader;
        _moduleImportUtil = moduleImportUtil;
        _scriptInitializer = new AsyncInitializer(InitializeScript);
    }

    private async ValueTask InitializeScript(CancellationToken token)
    {
        await _resourceLoader.LoadScriptAndWaitForVariable("https://challenges.cloudflare.com/turnstile/v0/api.js?render=explicit", "turnstile",
            cancellationToken: token);
        _ = await _moduleImportUtil.GetContentModuleReference(_wrapperModulePath, token);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
            await _scriptInitializer.Init(linked);
    }

    public async ValueTask<string> Create(DotNetObjectReference<Turnstile> dotnetObj, string elementId, TurnstileOptions options,
        InternalTurnstileOptions internalOptions, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            await _scriptInitializer.Init(linked);

            string optionsJson = JsonUtil.Serialize(options)!;
            string internalOptionsJson = JsonUtil.Serialize(internalOptions)!;

            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_wrapperModulePath, linked);
            return await module.InvokeAsync<string>("create", linked, elementId, optionsJson, internalOptionsJson, dotnetObj);
        }
    }

    public async ValueTask CreateObserver(string elementId, string widgetId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_wrapperModulePath, linked);
            await module.InvokeVoidAsync("createObserver", linked, elementId, widgetId);
        }
    }

    public async ValueTask Reset(string widgetId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_wrapperModulePath, linked);
            await module.InvokeVoidAsync("reset", linked, widgetId);
        }
    }

    public async ValueTask Remove(string widgetId, CancellationToken cancellationToken = default)
    {
        CancellationToken linked = _cancellationScope.CancellationToken.Link(cancellationToken, out CancellationTokenSource? source);

        using (source)
        {
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_wrapperModulePath, linked);
            await module.InvokeVoidAsync("remove", linked, widgetId);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_wrapperModulePath);

        await _scriptInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}