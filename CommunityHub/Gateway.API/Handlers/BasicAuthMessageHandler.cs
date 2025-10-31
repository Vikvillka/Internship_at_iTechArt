using System.Net.Http.Headers;
using System.Text;

namespace Gateway.API.Handlers;

public class BasicAuthMessageHandler : DelegatingHandler
{
    private readonly IConfiguration _config;

    public BasicAuthMessageHandler(IConfiguration config)
    {
        _config = config;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var username = _config["CommunityServiceApi:ApiCredentials:Username"];
        var password = _config["CommunityServiceApi:ApiCredentials:Password"];
        var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", token);
        return await base.SendAsync(request, cancellationToken);
    }
}
