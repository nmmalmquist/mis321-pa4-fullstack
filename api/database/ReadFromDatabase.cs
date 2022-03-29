using System;
using System.Collections.Generic;
using System.IO;
using api.interfaces;
using api.models;
using MySql.Data.MySqlClient;

namespace api.database
{
    public class ReadFromDatabase : IReadSongs
    {
        public List<Song> ReadAllSongs()
        {
            string cs = new ConnectionString().cs;
            MySqlConnection cnn = new MySqlConnection(cs);
            try
            {
                cnn.Open();


                string stm = "SELECT * FROM songs WHERE deleted != 'y' ORDER BY date_added DESC";
                using var cmd = new MySqlCommand(stm, cnn);

                using MySqlDataReader rdr = cmd.ExecuteReader();

                List<Song> allSongs = new List<Song>();
                while (rdr.Read())
                {
                    allSongs.Add(new Song()
                    {
                        SongID = rdr.GetInt32(0),
                        SongTitle = rdr.GetString(1),
                        SongTimestamp = rdr.GetDateTime(2),
                        Deleted = rdr.GetString(3),
                        Favorited = rdr.GetString(4)
                    });
                }
                cnn.Close();
                return allSongs;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.WriteLine($"Could not get all records from db");
                return new List<Song>();
            }


        }

        public Song ReadSong(int id)
        {
            string cs = new ConnectionString().cs;
            MySqlConnection cnn = new MySqlConnection(cs);
            try
            {
                cnn.Open();


                string stm = "SELECT * FROM songs WHERE id = @id AND deleted != 'y'";
                using var cmd = new MySqlCommand(stm, cnn);
                cmd.Parameters.AddWithValue("@id", id);

                using MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                List<Song> allSongs = new List<Song>();
                cnn.Close();
                return (new Song()
                {
                    SongID = rdr.GetInt32(0),
                    SongTitle = rdr.GetString(1),
                    SongTimestamp = rdr.GetDateTime(2),
                    Deleted = rdr.GetString(3)
                });



            }
            catch (Exception)
            {
                Console.WriteLine($"Could not get record {id} from db");
                return new Song() { SongTitle = "A song with that ID is does not exist" };
            }
        }

        public Song GetRecentOne()
        {
            string cs = new ConnectionString().cs;
            MySqlConnection cnn = new MySqlConnection(cs);
            try
            {
                cnn.Open();


                string stm = "SELECT * FROM songs ORDER BY date_added desc LIMIT 1";
                using var cmd = new MySqlCommand(stm, cnn);


                using MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();

                List<Song> allSongs = new List<Song>();
                cnn.Close();
                return (new Song()
                {
                    SongID = rdr.GetInt32(0),
                    SongTitle = rdr.GetString(1),
                    SongTimestamp = rdr.GetDateTime(2),
                    Deleted = rdr.GetString(3)
                });
            }
            catch (Exception)
            {
                Console.WriteLine("Could not get recent record from db");

                return new Song();
            }
        }
    }
}