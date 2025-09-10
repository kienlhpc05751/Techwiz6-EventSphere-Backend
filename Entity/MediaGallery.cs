using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EventSphere.Entity;

[Table("media_gallery")]
[Index("EventId", Name = "media_gallery_events_id_fk")]
[Index("UploadedBy", Name = "media_gallery_users_id_fk")]
public partial class MediaGallery
{
    [Key]
    [Column("id", TypeName = "bigint(20)")]
    public long Id { get; set; }

    [Column("event_id", TypeName = "bigint(20)")]
    public long EventId { get; set; }

    [Column("file_type", TypeName = "enum('image','video')")]
    public string FileType { get; set; } = null!;

    [Column("file_url")]
    [StringLength(255)]
    public string FileUrl { get; set; } = null!;

    [Column("uploaded_by", TypeName = "bigint(20)")]
    public long? UploadedBy { get; set; }

    [Column("caption")]
    [StringLength(150)]
    public string? Caption { get; set; }

    [Column("uploaded_on", TypeName = "datetime")]
    public DateTime UploadedOn { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("MediaGalleries")]
    public virtual Event Event { get; set; } = null!;

    [ForeignKey("UploadedBy")]
    [InverseProperty("MediaGalleries")]
    public virtual User? UploadedByNavigation { get; set; }
}
