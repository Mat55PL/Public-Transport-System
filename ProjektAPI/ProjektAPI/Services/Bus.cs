namespace ProjektAPI.Services;

public class Bus : IBus
{
    public int Id { get; set; } 
    
    public string Brand { get; set; } = String.Empty;
    
    public string Model { get; set; } = String.Empty;
}