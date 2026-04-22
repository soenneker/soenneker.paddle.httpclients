using Soenneker.Paddle.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Paddle.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PaddleOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IPaddleOpenApiHttpClient _httpclient;

    public PaddleOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IPaddleOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
