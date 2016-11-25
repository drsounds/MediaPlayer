using Bungalow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bungalow.Services
{
    /// <summary>
    /// A service for curation
    /// </summary>
    public interface ICuratorMusicService : IMusicService
    {
        List<Track> FeaturedTracksByGenre(string genre);
        List<Playlist> FeaturedPlaylistsByGenre(string genre);
        List<Playlist> FindPlaylistsForGenre(string genre);
        List<Artist> GetFeaturedArtistsForGenre(string genre);
        List<Genre> GetAvailableGenres(string genre);
    }
}
