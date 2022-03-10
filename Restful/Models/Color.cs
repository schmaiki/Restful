namespace Restful.Models;

public class Color
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}