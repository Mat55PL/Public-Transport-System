using System.ComponentModel.DataAnnotations;
namespace ProjektAPI.Services.Stop;

public class Stop : IStop
{
    public int Id { get; set; }
    [Required]
    public string StopName { get; set; }
    [Required]
    public string City { get; set; }
}