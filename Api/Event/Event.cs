using FastEndpoints;

namespace EventSphere.Api.Event;

public class Event: Group
{
    public Event()
    {
        Configure("/Event", _ => {});
    }
}
