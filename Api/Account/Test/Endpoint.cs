using FastEndpoints;

namespace EventSphere.Api.Account.Test;

internal sealed class Endpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/test");
        Group<Account>();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
