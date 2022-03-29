using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.models;
using api.database;
using api.interfaces;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        // GET: api/Song
        [HttpGet]
        public List<Song> Get()
        {
            IReadSongs readObj = new ReadFromDatabase();
            return readObj.ReadAllSongs();
        }

        // GET: api/Song/5
        [HttpGet("{id}", Name = "Get")]
        public Song Get(int id)
        {
            IReadSongs readObj = new ReadFromDatabase();
            return readObj.ReadSong(id);
        }

        // POST: api/Song
        [HttpPost]
        public void Post([FromBody] Song newSong)
        {
            ICreateSongs createObj = new WriteToDatabase();
            createObj.Create(newSong);
        }

        // PUT: api/Song/5
        [HttpPut]
        public void Put([FromBody] Song updatedSong)
        {
            IUpdateSongs updateObj = new UpdateDatabase();
            Console.WriteLine(updatedSong);
            updateObj.Update(updatedSong);
        }

        // DELETE: api/Song/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            IDeleteSongs deleteObj = new DeleteFromDatabase();
            deleteObj.Delete(id);
        }
    }
}
