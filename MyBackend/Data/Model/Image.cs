using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(512)]
        public string Url { get; set; } = string.Empty;

        [MaxLength(256)]
        public string FileName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string ContentType { get; set; } = string.Empty;

        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public required Section Section { get; set; }
    }
}
