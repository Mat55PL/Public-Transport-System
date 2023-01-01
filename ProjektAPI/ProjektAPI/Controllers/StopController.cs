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
        string connString = ConfigurationManager.AppSetting["connectionString"];
        using MySqlConnection connection = new(connString);
        try
        {
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Stops (StopName, City) VALUES (@StopName, @City)";
            command.Parameters.AddWithValue("@StopName", stop.StopName);
            command.Parameters.AddWithValue("@City", stop.City);
            command.ExecuteNonQuery();
            connection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new CustomException.InvalidDepartmentException("Error while adding stop: " + e.Message);
        }
    }

    [HttpDelete]
    [Route("DeleteStop")]
    public HttpResponseMessage DeleteStop(int id)
    {
        try
        {
            var connString = ConfigurationManager.AppSetting["connectionString"];
            using MySqlConnection connection = new MySqlConnection(connString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Stops WHERE StopID = @StopID";
            command.Parameters.AddWithValue("@StopID", id);
            command.ExecuteNonQuery();
            connection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new CustomException.InvalidDepartmentException("Error: " + e.Message);
        }
    }
    
    [HttpPut]
    [Route("UpdateStop")]
    public HttpResponseMessage UpdateStop([FromQuery] int id, [FromQuery] string stopName, [FromQuery] string city)
    {
        try
        {
           var connectionString = ConfigurationManager.AppSetting["connectionString"];
           var connection = new MySqlConnection(connectionString);
           connection.Open();
           MySqlCommand command = connection.CreateCommand();
           command.CommandText = "SELECT * FROM Stops WHERE StopID = @StopID";
           command.Parameters.AddWithValue("@StopID", id);
           MySqlDataReader reader = command.ExecuteReader();
           if (!reader.HasRows)
           {
               reader.Close();
               connection.Close();
               return new HttpResponseMessage(HttpStatusCode.NotFound);
           }
           if(stopName != null)
           {
               command.CommandText = "UPDATE Stops SET StopName = @StopName WHERE StopID = @StopID";
               command.Parameters.AddWithValue("@StopName", stopName);
           }
           if(city != null)
           { 
               command.CommandText = "UPDATE Stops SET City = @City WHERE StopID = @StopID";
               command.Parameters.AddWithValue("@City", city);
           }
           reader.Close();
           command.ExecuteNonQuery();
           connection.Close();
           return new HttpResponseMessage(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new CustomException.InvalidDepartmentException("Error: " + e.Message);
        }
    }
}