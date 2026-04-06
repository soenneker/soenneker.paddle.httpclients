using Soenneker.Paddle.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Paddle.HttpClients.Tests;

[Collection("Collection")]
public sealed class PaddleOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly IPaddleOpenApiHttpClient _httpclient;

    public PaddleOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IPaddleOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
