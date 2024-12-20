using Soenneker.Blazor.Turnstile.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Blazor.Turnstile.Tests;

[Collection("Collection")]
public class TurnstileInteropTests : FixturedUnitTest
{
    private readonly ITurnstileInterop _util;

    public TurnstileInteropTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ITurnstileInterop>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
