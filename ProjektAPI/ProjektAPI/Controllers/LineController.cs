using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Services.Line;

namespace ProjektAPI.Controllers;

public class LineController
{
    [HttpGet]
    [Route("GetLines")]
    public List<Line> GetLines(int id)
    {
        LineService lineService = new LineService();
        return lineService.GetLines(id);
    }
    
    [HttpPost]
    [Route("AddLine")]
    public HttpResponseMessage AddLine(Line line)
    {
        LineService lineService = new LineService();
        return lineService.AddLine(line);
    }
    
    [HttpPut]
    [Route("UpdateLine")]
    public HttpResponseMessage UpdateLine([FromQuery] int id, [FromQuery] string lineNumber, [FromQuery] int stopAid, [FromQuery] int stopBid)
    {
        LineService lineService = new LineService(); 
        return lineService.UpdateLine(id, lineNumber, stopAid, stopBid);
    }
    
    [HttpDelete]
    [Route("DeleteLine")]
    public HttpResponseMessage DeleteLine(int id)
    {
        LineService lineService = new LineService();
        return lineService.DeleteLine(id);
        
    }
}