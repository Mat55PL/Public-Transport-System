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
                command.CommandText = "INSERT INTO Buses (Brand, Model, Year) VALUES (@Brand, @Model, @Year)";
                //command.Parameters.AddWithValue("@Id", bus.Id);
                command.Parameters.AddWithValue("@Brand", bus.Brand);
                command.Parameters.AddWithValue("@Model", bus.Model);
                command.Parameters.AddWithValue("@Year", bus.Year);
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
            //check if bus exists
            using (MySqlConnection connection = new MySqlConnection(connString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Buses WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows) //if no rows returned 404
                {
                    connection.Close();
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                connection.Close();
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

    public HttpResponseMessage UpdateBus([FromQuery] int ID, [FromQuery] string Brand, [FromQuery] string Model, [FromQuery] int Year) // Pobiera wartości z ciągu zapytania.
    {
        try
        {
            var connectionString = ConfigurationManager.AppSetting["connectionString"];
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            //check if bus exists
            command.CommandText = "SELECT * FROM Buses WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", ID);
            MySqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows) //if no rows returned 404
            {
                connection.Close();
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            // checking the values are empty and if so not changing them
            if (Brand != null)
            {
                command.CommandText = "UPDATE Buses SET Brand = @Brand WHERE Id = @BusID";
                command.Parameters.AddWithValue("@Brand", Brand);
            }
            if (Model != null)
            {
                command.CommandText = "UPDATE Buses SET Model = @Model WHERE Id = @BusID";
                command.Parameters.AddWithValue("@Model", Model);
            }
            if (Year != 0)
            {
                command.CommandText = "UPDATE Buses SET Year = @Year WHERE Id = @BusID";
                command.Parameters.AddWithValue("@Year", Year);
            }
            command.Parameters.AddWithValue("@BusID", ID);
            command.ExecuteNonQuery();
            connection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            Console.WriteLine(e + "Error: " + e.Message);
            throw new CustomException.InvalidDepartmentException("Error: " + e.Message);
        }
    }
}