[![](https://img.shields.io/nuget/v/soenneker.paddle.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.paddle.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.paddle.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.paddle.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.paddle.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.paddle.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.paddle.httpclients/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.paddle.httpclients/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Paddle.HttpClients

Provides a cached `HttpClient` configured for Paddle's API, including bearer authentication and API-version pinning.

## Installation

```bash
dotnet add package Soenneker.Paddle.HttpClients
```

## Configuration

```json
{
  "Paddle": {
    "ApiKey": "your-server-side-api-key"
  }
}
```

`Paddle:ClientBaseUrl` can point to `https://sandbox-api.paddle.com` for sandbox keys. `Paddle:ApiVersion`, `Paddle:AuthHeaderName`, and `Paddle:AuthHeaderValueTemplate` can override their defaults.

## Usage

```csharp
using Soenneker.Paddle.HttpClients.Abstract;
using Soenneker.Paddle.HttpClients.Registrars;

services.AddPaddleOpenApiHttpClientAsSingleton();

IPaddleOpenApiHttpClient provider = serviceProvider
    .GetRequiredService<IPaddleOpenApiHttpClient>();

HttpClient client = await provider.Get(cancellationToken);
HttpResponseMessage response = await client.GetAsync("event-types", cancellationToken);
response.EnsureSuccessStatusCode();
```

The provider owns its cached client. Disposing the provider removes and disposes that client. Scoped registration gives each provider instance its own cached client.
