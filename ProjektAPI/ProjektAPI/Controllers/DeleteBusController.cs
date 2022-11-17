using System.Net;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProjektAPI.Services;

namespace ProjektAPI.Controllers;

public class DeleteBusController : ControllerBase
{
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
}