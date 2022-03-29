using System;
using System.Collections.Generic;
using System.IO;
using api.interfaces;
using api.models;
using MySql.Data.MySqlClient;

namespace api.database
{
    public class WriteToDatabase : ICreateSongs
    {
        public void Create(Song song)
        {
            string cs = new ConnectionString().cs;
            MySqlConnection cnn = new MySqlConnection(cs);
            try
            {
                cnn.Open();


                string stm = "INSERT INTO songs VALUES (0,@title,@date,@deleted,@fav)";
                using var cmd = new MySqlCommand(stm, cnn);
                cmd.Parameters.AddWithValue("@title", song.SongTitle);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@deleted", "n");
                cmd.Parameters.AddWithValue("@fav", song.Favorited);

                cmd.Prepare();
                cmd.ExecuteNonQuery();


                //cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }
    }
}