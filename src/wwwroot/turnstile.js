window.turnstileinterop = (function ()
{
    function create(elementId, optionsJson, internalOptionsJson, dotnetObj)
    {
        var options = JSON.parse(optionsJson);
        var internalOptions = JSON.parse(internalOptionsJson);

        var element = document.querySelector("#" + elementId);

        if (internalOptions.callbackEnabled)
            opt['callback'] = (token) => dotnetObj.invokeMethodAsync('Callback', token);

        if (internalOptions.onErrorEnabled)
            opt['error-callback'] = (message) => dotnetObj.invokeMethodAsync('ErrorCallback', message);

        if (internalOptions.onExpiredEnabled)
            opt['expired-callback'] = (message) => dotnetObj.invokeMethodAsync('ExpiredCallback', message);

        return turnstile.render(element, options);
    };

    return {
        create: create
    };
})();