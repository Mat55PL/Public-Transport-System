using System.Net;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProjektAPI.Services;
using ProjektAPI.Services.Bus;


namespace ProjektAPI.Controllers;

public class BusController 
{
    // GET
    [HttpGet]
    [Route("GetBuses")]
    public List<Bus> GetBuses(int id)
    { 
        BusService busService = new BusService();
        return busService.GetBuses(id);
    }
    
    // POST (SET)
    [HttpPost]
    [Route("AddBus")]
    
    public HttpResponseMessage AddBus (Bus bus)
    {
       BusService busService = new BusService();
       return busService.AddBus(bus);
    }
    
    // Delete
    [HttpDelete]
    [Route("DeleteBus")]
    public HttpResponseMessage DeleteBus(int id)
    {
        BusService busService = new BusService();
        return busService.DeleteBus(id);
    }
    
    //Update
    [HttpPut]
    [Route("UpdateBus")]

    public HttpResponseMessage UpdateBus([FromQuery] int ID, [FromQuery] string Brand, [FromQuery] string Model, [FromQuery] string Number, [FromQuery] int Year) // Pobiera wartości z ciągu zapytania.
    {
        BusService busService = new BusService();
        return busService.UpdateBus(ID, Brand, Model, Number, Year);
    }
}

