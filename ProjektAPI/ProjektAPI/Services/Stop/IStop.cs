namespace ProjektAPI.Services.Stop;

public interface IStop
{
    int Id { get; set; }
    
    string StopName { get; set; }
    
    string City { get; set; }
}