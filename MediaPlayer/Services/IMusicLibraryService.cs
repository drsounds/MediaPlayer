﻿using Bungalow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bungalow
{
    /// <summary>
    /// A library provider
    /// </summary>
    public interface IMusicLibraryService : IMusicService
    {
        List<Track> GetAllTracks();
        List<Track> GetTracksByArtist(string artist);
        List<Artist> GetAllArtists();
        List<Album> GetAlbumsByArtist(string artist);
        List<Track> GetTracksByAlbumFromArtist(string artist, string album);
        List<Track> GetTracksByUri(string query);
    }
}
