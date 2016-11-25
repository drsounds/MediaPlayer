using Bungalow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bungalow.Services
{
    /// <summary>
    /// Browsing music service
    /// </summary>
    public interface IMusicBrowsingService : IMusicService
    {
        List<Track> FindTracksByQuery(string query);
        List<Artist> GetTracksByArtist(string artist);
        List<Album> GetAlbumsByArtist(string artist);
        Album GetAlbumByName(string name);
        Album GetAlbumByUPC(string upc);
        List<Track> GetTracksOnAlbum(string artist, string album);
        Track LookupTrackByISRC(string isrc);
    }
}
