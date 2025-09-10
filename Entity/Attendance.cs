using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Entity;

[Table("attendance")]
public partial class Attendance
{
    [Key]
    [Column("registration_id", TypeName = "bigint(20)")]
    public long RegistrationId { get; set; }

    [Column("attended")]
    public bool Attended { get; set; }

    [Column("marked_on", TypeName = "datetime")]
    public DateTime MarkedOn { get; set; }

    [InverseProperty("Attendance")]
    public virtual Certificate? Certificate { get; set; }

    [ForeignKey("RegistrationId")]
    [InverseProperty("Attendance")]
    public virtual Registration Registration { get; set; } = null!;
}
