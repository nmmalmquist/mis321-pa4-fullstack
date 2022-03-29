using System;
using System.Collections.Generic;
using System.IO;
using api.interfaces;
using api.models;
using MySql.Data.MySqlClient;

namespace api.database
{
    public class DeleteFromDatabase : IDeleteSongs
    {
        public void Delete(int id)
        {
             string cs = new ConnectionString().cs;
            MySqlConnection cnn = new MySqlConnection(cs);
            try
            {
                cnn.Open();


                string stm = $"UPDATE songs SET deleted='y' WHERE id=@id";
                using var cmd = new MySqlCommand(stm, cnn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            cnn.Close();

               


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could notdelete record from db");
                Console.WriteLine(ex.Message);
               
              
            }
        }
    }
}