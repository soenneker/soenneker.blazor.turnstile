using Soenneker.Blazor.Turnstile.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Turnstile.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class TurnstileInteropTests : HostedUnitTest
{
    private readonly ITurnstileInterop _util;

    public TurnstileInteropTests(Host host) : base(host)
    {
        _util = Resolve<ITurnstileInterop>(true);
    }

    [Test]
    public void Default()
    {

    }
}
