using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Entity;

[Keyless]
public partial class EventsSeatsView
{
    [Column("id", TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column("total_seats", TypeName = "int(11)")]
    public int TotalSeats { get; set; }

    [Column("seats_booked", TypeName = "bigint(21)")]
    public long SeatsBooked { get; set; }

    [Column("seats_available", TypeName = "bigint(22)")]
    public long SeatsAvailable { get; set; }
}
