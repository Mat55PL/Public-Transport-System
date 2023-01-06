using System.Net;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProjektAPI.Services;
using ProjektAPI.Services.Stop;

namespace ProjektAPI.Controllers;

public class StopController : ControllerBase
{
    [HttpGet]
    [Route("GetStop")]
    public List<Stop> GetStops(int id)
    {
        StopService stopService = new();
        return stopService.GetStop(id);
    }
    
    [HttpPost]
    [Route("AddStop")]
    public HttpResponseMessage AddStop(Stop stop)
    { 
        StopService stopService = new();
        return stopService.AddStop(stop);
    }

    [HttpDelete]
    [Route("DeleteStop")]
    public HttpResponseMessage DeleteStop(int id)
    {
        StopService stopService = new();
        return stopService.DeleteStop(id);
    }
    
    [HttpPut]
    [Route("UpdateStop")]
    public HttpResponseMessage UpdateStop([FromQuery] int id, [FromQuery] string stopName, [FromQuery] string city)
    {
        StopService stopService = new();
        return stopService.UpdateStop(id, stopName, city);
    }
}