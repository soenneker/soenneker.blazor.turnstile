const interop = (() => {
    const instance = {};
    instance.observer = undefined;
    instance. = undefined;

    instance.create = async function(elementId, optionsJson, internalOptionsJson, dotnetObj) {
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

    instance.createObserver = function(elementId, widgetId) {
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
    };

    return instance;
})();
export function create(elementId, optionsJson, internalOptionsJson, dotnetObj) {
    return interop.create(elementId, optionsJson, internalOptionsJson, dotnetObj);
}

export function createObserver(elementId, widgetId) {
    return interop.createObserver(elementId, widgetId);
}

export function reset(widgetId) {
    return window.turnstile.reset(widgetId);
}

export function remove(widgetId) {
    return window.turnstile.remove(widgetId);
}