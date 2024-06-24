using Microsoft.JSInterop;
using System.Threading.Tasks;
using Soenneker.Blazor.Turnstile.Abstract;
using System.Threading;
using Soenneker.Blazor.Turnstile.Options;
using Soenneker.Utils.Json;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;

namespace Soenneker.Blazor.Turnstile;

///<inheritdoc cref="ITurnstileInterop"/>
internal class TurnstileInterop : ITurnstileInterop
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IModuleImportUtil _moduleImportUtil;

    public TurnstileInterop(IJSRuntime jsRuntime, IModuleImportUtil moduleImportUtil)
    {
        _jsRuntime = jsRuntime;
        _moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask<string> Create(DotNetObjectReference<Turnstile> dotnetObj, string elementId, TurnstileOptions options, InternalTurnstileOptions internalOptions,
        CancellationToken cancellationToken = default)
    {
        await _moduleImportUtil.Import("Soenneker.Blazor.Turnstile/js/turnstileinterop.js", cancellationToken);
        await _moduleImportUtil.WaitUntilLoaded("Soenneker.Blazor.Turnstile/js/turnstileinterop.js", cancellationToken);

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
        await _moduleImportUtil.DisposeModule("Soenneker.Blazor.Turnstile/js/turnstileinterop.js");
    }
}