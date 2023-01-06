namespace ProjektAPI.Services;

public interface IBus
{
    //interface for bus 
    int Id { get; set; }
    
    string? Brand { get; set; }
    
    string? Model { get; set; }
    
    string? Number { get; set; }
    int Year { get; set; }
}