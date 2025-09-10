using FastEndpoints;

namespace EventSphere.Api.Account.Login;

internal sealed record Request(string Email, string Password);

internal class Validator : Validator<Request>;

internal sealed record Response(string Email, string Token);
