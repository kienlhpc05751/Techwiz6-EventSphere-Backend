using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Entity;

[Table("user_details")]
public partial class UserDetail
{
    [Key]
    [Column("user_id", TypeName = "bigint(20)")]
    public long UserId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("phone")]
    [StringLength(10)]
    public string Phone { get; set; } = null!;

    [Column("department")]
    [StringLength(100)]
    public string Department { get; set; } = null!;

    [Column("enrollment_no")]
    [StringLength(50)]
    public string EnrollmentNo { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserDetail")]
    public virtual User User { get; set; } = null!;
}
