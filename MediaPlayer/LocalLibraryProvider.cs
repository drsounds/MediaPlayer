﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bungalow.Models;
using System.Text.RegularExpressions;

namespace Bungalow
{
    public class LocalLibraryProvider : IMusicLibraryService, IStreamingMusicService
    {
        public Track CurrentTrack
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Duration
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsPlaying
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Position
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public List<Album> GetAlbumsByArtist(string artist)
     
        {
            using (BungalowDatabaseContext dbContext = new BungalowDatabaseContext())
            {
                var xalbums = dbContext.Database.SqlQuery<string>("SELECT DISTINCT Album FROM tracks WHERE artist = '" + artist.Replace("'", "") + "'");
                List<Album> albums = new List<Album>();
                foreach (string salbum in xalbums)
                {
                    Album album = new Album() { Name = salbum };
                   albums.Add(album);

                }
                return albums;
            }
        }

        public List<Artist> GetAllArtists()
        {
            using (BungalowDatabaseContext dbContext = new BungalowDatabaseContext())
            {
                List<Artist> artists = new List<Artist>();
                var xartists = dbContext.Database.SqlQuery<string>("SELECT DISTINCT Artist from Tracks ORDER BY Artist ASC");

                foreach (string t in xartists)
                {
                    var artist = new Artist() { Name = t };
                    artists.Add(artist);
                }
                return artists.ToList();
            }
        }

        public List<Track> GetAllTracks()
        {
            using (BungalowDatabaseContext dbContext = new BungalowDatabaseContext())
            {
                var tracks = dbContext.Tracks.OrderBy((t) => t.Name).OrderBy((t) => t.Album).OrderBy((t) => t.Artist);
                return tracks.ToList();
            }
        }

        public List<Track> GetTracksByAlbumFromArtist(string artist, string album)
        {
            using (BungalowDatabaseContext dbContext = new BungalowDatabaseContext())
            {
                var tracks = dbContext.Tracks.OrderBy((t) => t.Name).OrderBy((t) => t.Album).OrderBy((t) => t.Artist).Where((t) => t.Album == album).Where((t) => t.Artist == artist);
                return tracks.ToList();
            }
        }

        public List<Track> GetTracksByArtist(string artist)
        {
            using (BungalowDatabaseContext dbContext = new BungalowDatabaseContext())
            {
                var tracks = dbContext.Tracks.OrderBy((t) => t.Name).OrderBy((t) => t.Album).OrderBy((t) => t.Artist).Where((t) => t.Artist == artist);
                return tracks.ToList();
            }
        }

        public List<Track> GetTracksByUri(string query)
        {
            if (new Regex("^urn:artist:(.*):track$").IsMatch(query))
            {
                var matches = new Regex("(urn:artist:)(.*)(:track)").Split(query);
                return this.GetTracksByArtist(matches[2]);
            }
            if (new Regex("^urn:artist:(.*):album$").IsMatch(query))
            {
                var matches = new Regex("(urn:artist:)(.*)(:album)").Split(query);
                return this.GetTracksByArtist(matches[2]);
            }
            if (new Regex("^urn:artist:(.*):album:(.*):track$").IsMatch(query))
            {
                var matches = new Regex("(urn:artist:)(.*)(:album:)(.*)(:track)$").Split(query);
                string artist = matches[2];
                string album = matches[4];
                return this.GetTracksByAlbumFromArtist(artist, album);
            }
            if (new Regex("urn:search:(.*)").IsMatch(query))
            {
                var q = query.Substring("urn:search:".Length);
                using (BungalowDatabaseContext dbContext = new BungalowDatabaseContext())
                {
                    var tracks = dbContext.Tracks.SqlQuery("SELECT * FROM Tracks WHERE Name LIKE '%" + q + "%' OR Artist LIKE '%" + q + "%' OR Album LIKE '%" + q + "%'");
                    return tracks.ToList();
                }
            }
            return new List<Track>();
        }

        public bool IsAvailable(Track track)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Play(Track track)
        {
            throw new NotImplementedException();
        }

        public bool Play(string name, string artist, string album)
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Seek(int pos)
        {
            throw new NotImplementedException();
        }
    }
}
