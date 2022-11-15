
using System.Configuration;
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
        List<Bus> buses = new List<Bus>();
        // connect to database mysql 
        string connectionString = "server=mysql-mattu.alwaysdata.net;user=mattu;password=Messi1010@;database=mattu_db";

        using (MySqlConnection con = new MySqlConnection(connectionString))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Buses", con);
            MySqlDataReader reader = cmd.ExecuteReader();
                
            while(reader.Read())
            {
                Bus bus = new Bus();
                bus.Id = reader.GetInt32("Id");
                bus.Brand = reader.GetString("Brand");
                bus.Model = reader.GetString("Model");
                buses.Add(bus);
            } 
            reader.Close();
        }

        return buses;
    }
}