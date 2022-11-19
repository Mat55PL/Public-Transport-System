using System.Net;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProjektAPI.Services;

namespace ProjektAPI.Controllers;

public class UpdateBusController : ControllerBase
{
    [HttpPut]
    [Route("UpdateBus")]

    public string UpdateBus([FromQuery] int ID, [FromQuery] string Brand, [FromQuery] string Model)
    {
        try
        {
            var connectionString = ConfigurationManager.AppSetting["connectionString"];
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Buses SET Brand = @Brand, Model = @Model WHERE Id = @BusID";
            command.Parameters.AddWithValue("@BusID", ID);
            command.Parameters.AddWithValue("@Brand", Brand);
            command.Parameters.AddWithValue("@Model", Model);
            command.ExecuteNonQuery();
            connection.Close();
            return "Bus updated";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    
    }
}