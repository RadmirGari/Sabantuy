using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Model;
public class Subscribers
{
    [Key]
    public required int Id { get; set; }
    [Required]
    public required string Name { get; set; } = String.Empty;
    [Required]
    public required string Email { get; set; } = String.Empty;
}