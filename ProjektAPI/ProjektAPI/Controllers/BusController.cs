using System.Net;
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
    
    // POST (SET)
    [HttpPost]
    [Route("AddBus")]
    
    public HttpResponseMessage Post (Bus bus)
    {
        string connString = ConfigurationManager.AppSetting["connectionString"]; //connection string from json file
        using (MySqlConnection connection = new MySqlConnection(connString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Buses (Brand, Model) VALUES (@Brand, @Model)";
                //command.Parameters.AddWithValue("@Id", bus.Id);
                command.Parameters.AddWithValue("@Brand", bus.Brand);
                command.Parameters.AddWithValue("@Model", bus.Model);
                command.ExecuteNonQuery();
                connection.Close();
                return new HttpResponseMessage(HttpStatusCode.OK); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new CustomException.InvalidDepartmentException("Error while adding bus: " + e.Message);
            }
        }
    }
    
    // Delete
    [HttpDelete]
    [Route("DeleteBus")]
    public HttpResponseMessage DeleteBus(int id)
    {
        try
        {
            string connString = ConfigurationManager.AppSetting["connectionString"]; //connection string from json file
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Buses WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new CustomException.InvalidDepartmentException("Error: " + e.Message);
        }
        
    }
    
    //Update
    [HttpPut]
    [Route("UpdateBus")]

    public string UpdateBus([FromQuery] int ID, [FromQuery] string Brand, [FromQuery] string Model) // Pobiera wartości z ciągu zapytania.
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
            return ("Bus ID: [" + ID + "] updated");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
            return e.Message;
        }
    }
}