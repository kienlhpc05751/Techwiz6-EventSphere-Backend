using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventSphere.Entity;

[Table("venues")]
public partial class Venue
{
    [Key]
    [Column("id", TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("maximum_seats", TypeName = "int(11)")]
    public int MaximumSeats { get; set; }

    [InverseProperty("Venue")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
