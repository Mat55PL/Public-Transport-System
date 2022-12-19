using MySql.Data.MySqlClient;

namespace ProjektAPI.Services.Stop;

public class StopService
{
    public List<Stop> GetStop(int id)
    {
        string connString = ConfigurationManager.AppSetting["connectionString"];
        Console.WriteLine("Id to: " + id);
        List<Stop> stops = new List<Stop>();
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            try
            {
                string sqlString = "SELECT * FROM Stops";
                if (id > 0)
                {
                    sqlString = "SELECT * FROM Stops WHERE StopId = @id";
                }
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Stop stop = new Stop();
                    stop.Id = reader.GetInt32("StopId");
                    stop.StopName = reader.GetString("StopName");
                    stop.City = reader.GetString("City");
                    stops.Add(stop);
                }
                con.Close();
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new CustomException.InvalidDepartmentException("Error while getting stop: " + e.Message);
            }

        }
        return stops;
    }
}