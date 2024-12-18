using Soenneker.Blazor.Turnstile.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Blazor.Turnstile.Tests;

[Collection("Collection")]
public class TurnstileTests : FixturedUnitTest
{
    private readonly ITurnstile _interop;

    public TurnstileTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _interop = Resolve<ITurnstile>(true);
    }
}
