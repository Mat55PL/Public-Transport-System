using System.ComponentModel.DataAnnotations;
namespace ProjektAPI.Services;

public class Bus : IBus
{
    
    public int Id { get; set; } 
    [Required]
    [StringLength(50)]
    public string Brand { get; set; } = string.Empty;
    [Required]
    [StringLength(50)]
    public string Model { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    [Required]
    [Range(1900, 2023)]
    public int Year { get; set; } = 2023;
}

