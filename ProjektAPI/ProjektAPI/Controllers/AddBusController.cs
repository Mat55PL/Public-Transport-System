﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProjektAPI.Services;

namespace ProjektAPI.Controllers;

public class AddBusController : ControllerBase
{
    // GET
    [HttpPost]
    [Route("AddBus")]
    
    public HttpResponseMessage Post (Bus bus)
    {
        string connectionString = "server=mysql-mattu.alwaysdata.net;user=mattu;password=Messi1010@;database=mattu_db";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Buses (Id, Brand, Model) VALUES (@Id, @Brand, @Model)";
                command.Parameters.AddWithValue("@Id", bus.Id);
                command.Parameters.AddWithValue("@Brand", bus.Brand);
                command.Parameters.AddWithValue("@Model", bus.Model);
                command.ExecuteNonQuery();
                connection.Close();
                return new HttpResponseMessage(HttpStatusCode.OK); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new CustomException.InvalidDepartmentException("Error while adding bus");
            }
             
        }
    }
}