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
        lineService.AddLine(line);
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
    
    [HttpPut]
    [Route("UpdateLine")]
    public HttpResponseMessage UpdateLine([FromQuery] int ID, [FromQuery] string LineNumber, [FromQuery] int StopAID, [FromQuery] int StopBID)
    {
        LineService lineService = new LineService();
        lineService.UpdateLine(ID, LineNumber, StopAID, StopBID);
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
    
    [HttpDelete]
    [Route("DeleteLine")]
    public HttpResponseMessage DeleteLine(int id)
    {
        LineService lineService = new LineService();
        lineService.DeleteLine(id);
        return new HttpResponseMessage(HttpStatusCode.OK);
    }
}