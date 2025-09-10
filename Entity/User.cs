using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Entity;

[Table("users")]
[Index("Email", Name = "users_pk_2", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id", TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("password")]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("role", TypeName = "enum('Participant','Organizer','Admin')")]
    public UserRole Role { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("Organizer")]
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    [InverseProperty("UploadedByNavigation")]
    public virtual ICollection<MediaGallery> MediaGalleries { get; set; } = new List<MediaGallery>();

    [InverseProperty("Student")]
    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();

    [InverseProperty("User")]
    public virtual UserDetail? UserDetail { get; set; }
}
