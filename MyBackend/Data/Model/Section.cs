using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model;
public class Section
{
    [Key]
    public required int Id { get; set; }
    [Required]
    public required string Name { get; set; } = String.Empty;
    [Required]
    public required string Information { get; set; } = String.Empty;

    public List<Image> Images { get; set; } = new List<Image>();
}