namespace ProjektAPI.Services.Line;

public interface ILine
{
    int LineId { get; set; }
    string LineNumber { get; set; }
    int StopAid { get; set; }
    string StopAName { get; set; }
    int StopBid { get; set; }
    string StopBName { get; set; }
}