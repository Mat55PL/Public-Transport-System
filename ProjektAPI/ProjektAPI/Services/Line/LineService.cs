using System.Net;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace ProjektAPI.Services.Line;

public class LineService
{
    public List<ILine> GetLines(int id)
    {
        string connString = DbManager.AppSetting["connectionString"];
        List<ILine> lines = new List<ILine>();
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            try
            {
                var sqlString =
                    "SELECT Line.LineId, Line.LineNumber, Line.StopAID, StopA.StopName AS 'StopA', Line.StopBID, StopB.StopName AS 'StopB' FROM Line";
                sqlString +=
                    " JOIN Stops AS StopA ON Line.StopAID = StopA.StopId JOIN Stops AS StopB ON Line.StopBID = StopB.StopId";
                if (id > 0)
                {
                    sqlString += " WHERE LineId = @id";
                }
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sqlString, con);
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Line line = new Line();
                    line.LineId = Convert.ToInt32(reader["LineId"]);
                    line.LineNumber = Convert.ToString(reader["LineNumber"]);
                    line.StopAid = Convert.ToInt32(reader["StopAID"]);
                    line.StopAName = Convert.ToString(reader["StopA"]);
                    line.StopBid = Convert.ToInt32(reader["StopBID"]);
                    line.StopBName = Convert.ToString(reader["StopB"]);
                    lines.Add(line);
                }

                con.Close();
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new CustomException.InvalidDepartmentException("Error while getting lines: " + e.Message);
            }
        }

        return lines;
    }

    //function add new line
    public HttpResponseMessage AddLine(Line line)
    {
        string connString = DbManager.AppSetting["connectionString"];
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            try
            {
                con.Open();
                MySqlCommand cmd =
                    new MySqlCommand(
                        "INSERT INTO Line (LineNumber, StopAID, StopBID) VALUES (@LineNumber, @StopAID, @StopBID)",
                        con);
                cmd.Parameters.AddWithValue("@LineNumber", line.LineNumber);
                cmd.Parameters.AddWithValue("@StopAID", line.StopAid);
                cmd.Parameters.AddWithValue("@StopBID", line.StopBid);
                cmd.ExecuteNonQuery();
                con.Close();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while adding line: " + e.Message);
                throw new CustomException.InvalidDepartmentException("Error while adding line: " + e.Message);
            }
        }
    }
    
    //function update line
    public HttpResponseMessage UpdateLine([FromQuery] int ID, [FromQuery] string LineNumber, [FromQuery] int StopAID, [FromQuery] int StopBID)
    {
        try
        {
            string connString = DbManager.AppSetting["connectionString"];
            var connection = new MySqlConnection(connString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Bus WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", ID);
            MySqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows) //if no rows returned 404
            {
                connection.Close();
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            if (LineNumber != null)
            {
                command.CommandText = "UPDATE Line SET LineNumber = @LineNumber WHERE LineId = @Id";
                command.Parameters.AddWithValue("@LineNumber", LineNumber);
            }

            if (StopAID != 0)
            {
                command.CommandText = "UPDATE Line SET StopAID = @StopAID WHERE LineId = @Id";
                command.Parameters.AddWithValue("@StopAID", StopAID);
            }

            if (StopBID != 0)
            {
                command.CommandText = "UPDATE Line SET StopBID = @StopBID WHERE LineId = @Id";
                command.Parameters.AddWithValue("@StopBID", StopBID);
            }

            reader.Close();
            command.ExecuteNonQuery();
            connection.Close();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while updating line: " + e.Message);
            throw new CustomException.InvalidDepartmentException("Error while updating line: " + e.Message);
        }
    }

    //function delete line
    public HttpResponseMessage DeleteLine(int id)
    {
        string connString = DbManager.AppSetting["connectionString"];
        using (MySqlConnection con = new MySqlConnection(connString))
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM Line WHERE LineId = @LineId", con);
                cmd.Parameters.AddWithValue("@LineId", id);
                cmd.ExecuteNonQuery();
                con.Close();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while deleting line: " + e.Message);
                throw new CustomException.InvalidDepartmentException("Error while deleting line: " + e.Message);
            }
        }
    }
}