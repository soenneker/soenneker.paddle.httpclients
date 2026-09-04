using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Paddle.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Paddle.HttpClients;

/// <inheritdoc cref="IPaddleOpenApiHttpClient" />
public sealed class PaddleOpenApiHttpClient : IPaddleOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _clientId = $"{nameof(PaddleOpenApiHttpClient)}:{Guid.NewGuid():N}";

    private const string _prodBaseUrl = "https://api.paddle.com";

    public PaddleOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_clientId, (config: _config, baseUrl: _config["Paddle:ClientBaseUrl"] ?? _prodBaseUrl), static state =>
        {
            var apiKey = state.config.GetValueStrict<string>("Paddle:ApiKey");
            string authHeaderName = state.config["Paddle:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = state.config["Paddle:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);
            string apiVersion = state.config["Paddle:ApiVersion"] ?? "1";

            return new HttpClientOptions
            {
                BaseAddress = new Uri(state.baseUrl),
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {authHeaderName, authHeaderValue},
                    {"Paddle-Version", apiVersion},
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_clientId);
    }
}
