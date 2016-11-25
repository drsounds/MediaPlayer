using System.Collections.Generic;
using Bungalow.Models;

namespace Bungalow
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