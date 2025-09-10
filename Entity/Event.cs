using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Entity;

[Table("events")]
[Index("OrganizerId", Name = "events_users_id_fk")]
[Index("VenueId", Name = "events_venues_id_fk")]
public partial class Event
{
    [Key]
    [Column("id", TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("time", TypeName = "time")]
    public TimeOnly Time { get; set; }

    [Column("total_seats", TypeName = "int(11)")]
    public int TotalSeats { get; set; }

    [Column("venue_id", TypeName = "bigint(20)")]
    public long? VenueId { get; set; }

    [Column("organizer_id", TypeName = "bigint(20)")]
    public long? OrganizerId { get; set; }

    [Column("waitlist_enabled")]
    public bool WaitlistEnabled { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<MediaGallery> MediaGalleries { get; set; } = new List<MediaGallery>();

    [ForeignKey("OrganizerId")]
    [InverseProperty("Events")]
    public virtual User? Organizer { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    [ForeignKey("VenueId")]
    [InverseProperty("Events")]
    public virtual Venue? Venue { get; set; }
}
