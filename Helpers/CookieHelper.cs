using FastEndpoints.Security;

namespace EventSphere.Helpers;

public static class CookieHelper
{
    public static string AppendToken(this IResponseCookies cookies, string tokenName, Action<JwtCreationOptions> options)
    {
        string token = JwtBearer.CreateToken(options);
        var opts = new JwtCreationOptions();
        options.Invoke(opts);
        cookies.Append(tokenName, token, new CookieOptions
        {
            Expires = opts.ExpireAt,
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });
        return token;
    }
}
