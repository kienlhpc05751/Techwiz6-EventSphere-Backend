using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Entity;

[Table("registrations")]
[Index("EventId", "StudentId", Name = "registrations_pk_2", IsUnique = true)]
[Index("StudentId", Name = "registrations_users_id_fk")]
public partial class Registration
{
    [Key]
    [Column("id", TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column("event_id", TypeName = "bigint(20)")]
    public long EventId { get; set; }

    [Column("student_id", TypeName = "bigint(20)")]
    public long? StudentId { get; set; }

    [Column("registered_on", TypeName = "datetime")]
    public DateTime RegisteredOn { get; set; }

    [Column("status", TypeName = "enum('Confirmed','Cancelled','Waitlisted')")]
    public RegistrationStatus Status { get; set; }

    [InverseProperty("Registration")]
    public virtual Attendance? Attendance { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("Registrations")]
    public virtual Event Event { get; set; } = null!;

    [InverseProperty("Registration")]
    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    [ForeignKey("StudentId")]
    [InverseProperty("Registrations")]
    public virtual User? Student { get; set; }
}
