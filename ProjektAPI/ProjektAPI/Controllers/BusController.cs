using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace ProjektAPI.Controllers;

public class BusController : ControllerBase
{
    // GET
    [HttpGet]
    [Route("GetBuses")]
    public string GetBuses()
    {
            // connect to database mysql 
        using (MySqlConnection con = new MySqlConnection("server=mysql-mattu.alwaysdata.net;user=mattu;password=Messi1010@;database=mattu_db"))
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM test", con);
            MySqlDataReader reader = cmd.ExecuteReader();
                
            while(reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
            } 
        }
            return "Solaris Urbino 12";
    }
}