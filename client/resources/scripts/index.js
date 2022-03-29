BASE_URL = "https://localhost:7145/";
let playlist = [];

const renderMusicImages = async () => {
  await getAllSongs();
  updateDOMwithSongs();
};

const getAllSongs = async () => {
  const url = BASE_URL + "api/song";
  const res = await fetch(url);
  const json = await res.json();
  playlist = json;
  return json;
};

const updateDOMwithSongs = () => {
  let html = "";
  playlist.forEach((song) => {
    console.log(song)
    html += `<div class=\"col playlistColumn\"><h3>${song.songTitle}</h3>
        <img class="trackImage" src="./resources/images/music.jpeg"><h6>Favorited: ${song.favorited}</h6></div>`;
    document.getElementById("playlistGrid").innerHTML = html;
  });
};

const addSong = async () => {
  const url = BASE_URL + "api/song";
  const newSong = document.getElementById("addTitle").value;
  const fav = document.getElementById("addFav").value;

  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ SongTitle: newSong, Favorited: fav }),
  });

  await renderMusicImages();
};


const updateSong = async () => {
  const url = BASE_URL + "api/song";
  const songId = document.getElementById("updateid").value;
  const title = document.getElementById("updateTitle").value;
  const timestamp = document.getElementById("updateTimestamp").value;
  const fav = document.getElementById("updateFav").value;

  if (!validId(songId)) {
    console.log("in")
    const errElement = document.createElement("h6");
    errElement.textContent = "You did not enter a valid ID to delete";
    errElement.style.color = "red";
    document.getElementById("updateError").append(errElement);
    return;
  }
  
  const response = await fetch(url, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ SongID: songId, SongTitle: title, SongTimestamp: timestamp, Favorited: fav }),
  });
  console.log(response)
  

  await renderMusicImages();
  document.getElementById("updatemodalclose").click();
};


const deleteSong = async () => {
  const id = document.getElementById("deleteid").value;
  
  if (!validId(id)) {
    console.log("in")
    const errElement = document.createElement("h6");
    errElement.textContent = "You did not enter a valid ID to delete";
    errElement.style.color = "red";
    document.getElementById("deleteError").append(errElement);
    return;
  }

  const url = BASE_URL + `api/song/${id}`;

  const response = await fetch(url, {
    method: "DELETE",
  });

  await renderMusicImages();
  document.getElementById("deletemodalclose").click();
};

const populateListSongs = (type) => {
  let html = "";
  playlist.forEach((song) => {
    html += `
      <h6> ID: ${song.songID} Title: ${song.songTitle}</h6>
    `;
    document.getElementById(`listSongs${type}`).innerHTML = html;
  });
};

const validId = (id) => {
  let found = false;
  playlist.forEach((song) => {
    if (song.songID.toString() == id){
      found = true
    };
  });
  return found;
};
