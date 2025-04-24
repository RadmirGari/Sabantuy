using System.ComponentModel.DataAnnotations;

namespace Model
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
    }
}
