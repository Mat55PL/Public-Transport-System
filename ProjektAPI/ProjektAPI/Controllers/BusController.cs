using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProjektAPI.Services;

namespace ProjektAPI.Controllers;


public class BusController : ControllerBase
{
    // GET
    [HttpGet]
    [Route("GetBuses")]
    public List<Bus> GetBuses()
    {
        BusService busService = new();
        return busService.GetBuses();
    }
}