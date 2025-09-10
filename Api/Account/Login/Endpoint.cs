using EventSphere.Helpers;
using FastEndpoints;

namespace EventSphere.Api.Account.Login;

internal sealed class Endpoint : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
        Description(desc =>
        {
            desc.ProducesProblemFE(StatusCodes.Status401Unauthorized);
        });
        Group<Account>();
    }

    public override Task HandleAsync(Request req, CancellationToken ct)
    {
        //
        // if (req.Email != Config["Admin:Username"] || req.Password != Config["Admin:Password"])
        // {
        //     ThrowError("The supplied credentials are invalid!", StatusCodes.Status401Unauthorized);
        // }
        //
        // Response = new Response(req.Email, HttpContext.Response.Cookies.AppendToken(Config["Jwt:CookieName"]!, o =>
        // {
        //     o.SigningKey = Config["Jwt:Key"]!;
        //     o.ExpireAt = DateTime.UtcNow.AddHours(8);
        //     o.User.Roles.Add("Admin");
        //     o.User.Claims.Add(("Username", req.Email));
        // }));
        
        return Task.CompletedTask;
    }
}