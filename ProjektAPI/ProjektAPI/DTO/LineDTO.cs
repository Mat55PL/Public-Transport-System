namespace ProjektAPI.DTO;

public class LineDTO
{
    public int LineId { get; set; }
    public string LineNumber { get; set; }
    public int StopAid { get; set; }
    public string StopAName { get; set; } 
    public int StopBid { get; set; }
    public string StopBName { get; set; } 
}