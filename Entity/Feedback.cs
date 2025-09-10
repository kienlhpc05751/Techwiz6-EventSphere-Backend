using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Entity;

[Table("feedback")]
[Index("RegistrationId", Name = "feedback_registrations_id_fk")]
public partial class Feedback
{
    [Key]
    [Column("id", TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column("registration_id", TypeName = "bigint(20)")]
    public long RegistrationId { get; set; }

    [Column("rating", TypeName = "tinyint(4)")]
    public sbyte Rating { get; set; }

    [Column("comments", TypeName = "text")]
    public string? Comments { get; set; }

    [Column("submitted_on", TypeName = "datetime")]
    public DateTime SubmittedOn { get; set; }

    [ForeignKey("RegistrationId")]
    [InverseProperty("Feedbacks")]
    public virtual Registration Registration { get; set; } = null!;
}
