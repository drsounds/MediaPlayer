using System.Collections.Generic;
using MediaPlayer.Models;

namespace MediaPlayer
{
    public interface ILocalLibraryProvider
    {
        List<Album> GetAlbumsByArtist(string artist);
        List<Artist> GetAllArtists();
        List<Track> GetAllTracks();
        List<Track> GetTracksByAlbumFromArtist(string artist, string album);
        List<Track> GetTracksByArtist(string artist);
        List<Track> GetTracksByUri(string query);
    }
}