const turnstileObservers = new Map();

export async function create(elementId, optionsJson, internalOptionsJson, dotnetObj) {
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
}

export function createObserver(elementId, widgetId) {
    const target = document.getElementById(elementId);

    const existingObserver = turnstileObservers.get(widgetId);
    existingObserver?.disconnect();

    const observer = new MutationObserver(function (mutations) {
        const targetRemoved = mutations.some(function (mutation) {
            const nodes = Array.from(mutation.removedNodes);
            return nodes.indexOf(target) !== -1;
        });

        if (targetRemoved) {
            window.turnstile.remove(widgetId);
            observer.disconnect();
            turnstileObservers.delete(widgetId);
        }
    });

    turnstileObservers.set(widgetId, observer);
    observer.observe(target.parentNode, { childList: true });
}

export function reset(widgetId) {
    return window.turnstile.reset(widgetId);
}

export function remove(widgetId) {
    const observer = turnstileObservers.get(widgetId);
    observer?.disconnect();
    turnstileObservers.delete(widgetId);

    return window.turnstile.remove(widgetId);
}
