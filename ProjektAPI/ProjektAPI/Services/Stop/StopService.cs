using System.Net;
using MySql.Data.MySqlClient;

namespace ProjektAPI.Services.Stop;

public class StopService
{
    public List<IStop> GetStop(int id)
    {
        string connString = DbManager.AppSetting["connectionString"];
        Console.WriteLine("Id to: " + id);
        List<IStop> stops = new List<IStop>();
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

    public HttpResponseMessage AddStop(Stop stop)
    {
        string connString = DbManager.AppSetting["connectionString"];
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            try
            {
                con.Open();
                string sqlString = "INSERT INTO Stops (StopName, City) VALUES (@stopName, @city)";
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                cmd.Parameters.AddWithValue("@stopName", stop.StopName);
                cmd.Parameters.AddWithValue("@city", stop.City);
                cmd.ExecuteNonQuery();
                con.Close();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while adding stop: " + e);
                throw new CustomException.InvalidDepartmentException("Error while adding stop: " + e.Message);
            }
        }
    }

    public HttpResponseMessage UpdateStop(int id, string stopName, string city)
    {
        try
        {
            string connString = DbManager.AppSetting["connectionString"];
            var conn = new MySqlConnection(connString);
            conn.Open();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Stops WHERE StopId = @id";
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                conn.Close();
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            if(stopName != null)
            {
                command.CommandText = "UPDATE Stops SET StopName = @stopName WHERE StopId = @id";
                command.Parameters.AddWithValue("@stopName", stopName);
            }
            if(city != null)
            {
                command.CommandText = "UPDATE Stops SET City = @city WHERE StopId = @id";
                command.Parameters.AddWithValue("@city", city);
            }
            reader.Close();
            command.ExecuteNonQuery();
            conn.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while updating stop: " + e);
            throw new CustomException.InvalidDepartmentException("Error while updating stop: " + e.Message);
        }
    }

    public HttpResponseMessage DeleteStop(int id)
    {
        string connString = DbManager.AppSetting["connectionString"];
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            try
            {
                con.Open();
                string sqlString = "DELETE FROM Stops WHERE StopId = @id";
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.Close();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while deleting stop: " + e);
                throw new CustomException.InvalidDepartmentException("Error while deleting stop: " + e.Message);
            }
        }
    }
}