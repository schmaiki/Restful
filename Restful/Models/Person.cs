namespace Restful.Models;

public class Person
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(100)]
    public string? LastName { get; set; }

    [Required]
    public int? ZipCode { get; set; }


    [Required]
    [StringLength(100)]
    public string? City { get; set; }

    [Required]
    [StringLength(100)]
    public Color Colors { get; set; }
}