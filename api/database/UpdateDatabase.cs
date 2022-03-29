using System;
using System.Collections.Generic;
using System.IO;
using api.interfaces;
using api.models;
using MySql.Data.MySqlClient;

namespace api.database
{
    public class UpdateDatabase : IUpdateSongs
    {
        public void Update(Song song)
        {
            string cs = new ConnectionString().cs;
            MySqlConnection cnn = new MySqlConnection(cs);
            try
            {
                cnn.Open();


                string stm = $"UPDATE songs SET title=@title,date_added=@date, favorited=@fav WHERE id=@id";
                using var cmd = new MySqlCommand(stm, cnn);
                cmd.Parameters.AddWithValue("@title", song.SongTitle);
                cmd.Parameters.AddWithValue("@date", song.SongTimestamp);
                cmd.Parameters.AddWithValue("@id", song.SongID);
                cmd.Parameters.AddWithValue("@fav", song.Favorited);

                cmd.ExecuteNonQuery();

            cnn.Close();
            }
            catch (Exception)
            {
                Console.WriteLine($"Could not update record {song.SongID} from db");
               

            }
        }
    }
}