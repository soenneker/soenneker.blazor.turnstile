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

/// <inheritdoc cref="ITurnstileInterop"/>
public sealed class TurnstileInterop : ITurnstileInterop
{
    private readonly System.Text.Json.JsonSerializerOptions _jsonOptions;

    private System.Text.Json.Serialization.Metadata.JsonTypeInfo<T> GetJsonTypeInfo<T>() =>
        (System.Text.Json.Serialization.Metadata.JsonTypeInfo<T>)_jsonOptions.GetTypeInfo(typeof(T));

    private readonly IResourceLoader _resourceLoader;
    private readonly IModuleImportUtil _moduleImportUtil;

    private readonly AsyncInitializer _scriptInitializer;

    private const string _wrapperModulePath = "_content/Soenneker.Blazor.Turnstile/js/turnstileinterop.js";

    private readonly CancellationScope _cancellationScope = new();

    public TurnstileInterop(IResourceLoader resourceLoader, IModuleImportUtil moduleImportUtil, System.Text.Json.Serialization.JsonSerializerContext? jsonContext = null)
    {
        _jsonOptions = LibraryJsonContext.WithContext(jsonContext);
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

            string optionsJson = JsonUtil.Serialize(options, GetJsonTypeInfo<TurnstileOptions>())!;
            string internalOptionsJson = JsonUtil.Serialize(internalOptions, GetJsonTypeInfo<InternalTurnstileOptions>())!;

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

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_wrapperModulePath);

        await _scriptInitializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}