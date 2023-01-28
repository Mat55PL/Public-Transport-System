using System.Net;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace ProjektAPI.Services.Bus;

public class BusService
{
    public List<Bus> GetBuses(int id)
    {
        List<Bus> buses = new List<Bus>();
        // connect to database mysql 
        string connString = DbManager.AppSetting["connectionString"]; //connection string from json file
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            try
            {
                string sqlString = "SELECT * FROM Bus";
                if(id>0)
                {
                    sqlString += " WHERE Id = @Id";
                }
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                if(id>0)
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                }
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Bus bus = new Bus();
                    bus.Id = Convert.ToInt32(reader["Id"]);
                    bus.Brand = reader["Brand"].ToString();
                    bus.Model = reader["Model"].ToString();
                    bus.Year = Convert.ToInt32(reader["Year"]);
                    bus.Number = reader["Number"].ToString();
                    buses.Add(bus);
                }
                con.Close();
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new CustomException.InvalidDepartmentException("Error while getting bus: " + e.Message);
            }
        }
        return buses;
    }

    public HttpResponseMessage AddBus(Bus bus)
    {
        string connString = DbManager.AppSetting["connectionString"];
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Bus (Brand, Model, Year, Number) VALUES (@Brand, @Model, @Year, @Number)", con);
                cmd.Parameters.AddWithValue("@Brand", bus.Brand);
                cmd.Parameters.AddWithValue("@Model", bus.Model);
                cmd.Parameters.AddWithValue("@Year", bus.Year);
                cmd.Parameters.AddWithValue("@Number", bus.Number);
                cmd.ExecuteNonQuery();
                con.Close();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while adding bus: " + e);
                throw new CustomException.InvalidDepartmentException("Error while adding bus: " + e.Message);
            }
        }
    }

    public HttpResponseMessage UpdateBus(int id, string brand, string model, string number, int year)
    {
        try
        {
            string connString = DbManager.AppSetting["connectionString"];
            var connection = new MySqlConnection(connString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Bus WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows) //if no rows returned 404
            {
                connection.Close();
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            if(brand != null)
            {
                command.CommandText = "UPDATE Bus SET Brand = @Brand WHERE Id = @Id";
                command.Parameters.AddWithValue("@Brand", brand);
            }
            if(model != null)
            {
                command.CommandText = "UPDATE Bus SET Model = @Model WHERE Id = @Id";
                command.Parameters.AddWithValue("@Model", model);
            }
            if(number != null)
            {
                command.CommandText = "UPDATE Bus SET Number = @Number WHERE Id = @Id";
                command.Parameters.AddWithValue("@Number", number);
            }
            if(year != 0)
            {
                command.CommandText = "UPDATE Bus SET Year = @Year WHERE Id = @Id";
                command.Parameters.AddWithValue("@Year", year);
            }
            reader.Close();
            command.ExecuteNonQuery();
            connection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while updating bus: " + e.Message);
            throw new CustomException.InvalidDepartmentException("Error while updating bus: " + e.Message);
        }
    }
    
    public HttpResponseMessage DeleteBus(int id)
    {
        string connString = DbManager.AppSetting["connectionString"];
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Bus WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                con.Close();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while deleting bus: " + e.Message);
                throw new CustomException.InvalidDepartmentException("Error while deleting bus: " + e.Message);
            }
        }
    }
}