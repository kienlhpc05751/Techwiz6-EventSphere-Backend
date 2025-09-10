using FastEndpoints;

namespace EventSphere.Api.Account;

public class Account: Group
{
    public Account()
    {
        Configure("/account", _ => {});
    }
}
