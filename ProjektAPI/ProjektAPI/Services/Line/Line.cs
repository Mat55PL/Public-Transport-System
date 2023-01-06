using System.ComponentModel.DataAnnotations;

namespace ProjektAPI.Services.Line;

public class Line : ILine
{

    public int LineId { get; set; }

    [Required]
    [StringLength(50)]
    public string LineNumber { get; set; }

    [Required]
    public int StopAid { get; set; }
    
    public string StopAName { get; set; } = string.Empty;

    [Required]
    public int StopBid { get; set; }
    
    public string StopBName { get; set; } = string.Empty;
}