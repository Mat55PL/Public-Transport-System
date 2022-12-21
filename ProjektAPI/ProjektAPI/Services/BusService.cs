namespace ProjektAPI.Services;
using MySql.Data.MySqlClient;
public class BusService
{
    public List<Bus> GetBuses()
    {
        List<Bus> buses = new List<Bus>();
        // connect to database mysql 
        string connString = ConfigurationManager.AppSetting["connectionString"]; //connection string from json file
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Bus", con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Bus bus = new Bus();
                bus.Id = reader.GetInt32("Id");
                bus.Brand = reader.GetString("Brand");
                bus.Model = reader.GetString("Model");
                bus.Number = reader.GetString("Number");
                bus.Year = reader.GetInt32("Year");
                buses.Add(bus);
            }
            con.Close();
            reader.Close();
        }
        return buses;
    }
}