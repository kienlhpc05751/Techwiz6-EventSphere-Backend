using FastEndpoints;

namespace EventSphere.Api.Account.Logout;

[HttpPost("/logout")]
[Group<Account>]
internal sealed class Endpoint : EndpointWithoutRequest
{
    public override Task HandleAsync(CancellationToken ct)
    {
        HttpContext.Response.Cookies.Delete(Config["Jwt:CookieName"]!);
        return Task.CompletedTask;
    }
}