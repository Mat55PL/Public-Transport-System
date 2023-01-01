using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
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

}