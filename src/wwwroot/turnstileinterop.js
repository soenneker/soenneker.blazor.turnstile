export class TurnstileInterop
{
    observer;

    async create(elementId, optionsJson, internalOptionsJson, dotnetObj)
    {
        var options = JSON.parse(optionsJson);
        var internalOptions = JSON.parse(internalOptionsJson);

        var element = document.getElementById(elementId);

        if (internalOptions.callbackEnabled)
            options['callback'] = (token) => dotnetObj.invokeMethodAsync('Callback', token);

        if (internalOptions.onErrorEnabled)
            options['error-callback'] = (message) => dotnetObj.invokeMethodAsync('ErrorCallback', message);

        if (internalOptions.onExpiredEnabled)
            options['expired-callback'] = (message) => dotnetObj.invokeMethodAsync('ExpiredCallback', message);

        return window.turnstile.render(element, options);
    };

    createObserver(elementId, widgetId) {
        const target = document.getElementById(elementId);

        this.observer = new MutationObserver(function (mutations) {
            const targetRemoved = mutations.some(function (mutation) {
                const nodes = Array.from(mutation.removedNodes);
                return nodes.indexOf(target) !== -1;
            });

            if (targetRemoved) {
                turnstile.remove(widgetId);

                // Disconnect and delete MutationObserver
                this.observer && this.observer.disconnect();
                delete this.observer;
            }
        });

        this.observer.observe(target.parentNode, { childList: true });
    }
}

window.TurnstileInterop = new TurnstileInterop();