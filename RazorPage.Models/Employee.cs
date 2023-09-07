using System.ComponentModel.DataAnnotations;

namespace RazorPage.Models;

public class Employee
{
    public int Id { get; set; }
    
    [MaxLength(50),MinLength(2)]
    [Required(ErrorMessage = "The name field can't be null. Please, write the name")]
    public string Name { get; set; }
    
    [MaxLength(50),MinLength(2)]
    [Required]
    [RegularExpression("^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$",ErrorMessage = "Please, enter a Valid Email (format: example@exame.com)")]
    public string Email { get; set; }
    public string? PhotoPath { get; set; }
    public Dept? Department { get; set; }
}