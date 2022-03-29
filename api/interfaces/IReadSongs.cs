using System.Collections.Generic;
using api.models;

namespace api.interfaces
{
    public interface IReadSongs
    {
         List<Song> ReadAllSongs();
         Song ReadSong(int id);
    }
}