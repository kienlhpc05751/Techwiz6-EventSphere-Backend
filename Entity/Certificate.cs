using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Entity;

[Table("certificates")]
public partial class Certificate
{
    [Key]
    [Column("attendance_id", TypeName = "bigint(20)")]
    public long AttendanceId { get; set; }

    [Column("certificate_url")]
    [StringLength(255)]
    public string CertificateUrl { get; set; } = null!;

    [Column("issued_on", TypeName = "datetime")]
    public DateTime IssuedOn { get; set; }

    [ForeignKey("AttendanceId")]
    [InverseProperty("Certificate")]
    public virtual Attendance Attendance { get; set; } = null!;
}
