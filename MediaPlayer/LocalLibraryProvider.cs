using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaPlayer.Models;
using System.Text.RegularExpressions;

namespace MediaPlayer
{
    public class LocalLibraryProvider : ILibraryProvider
    {
        public List<Album> GetAlbumsByArtist(string artist)
     
        {
            using (MediaPlayerDatabaseContext dbContext = new MediaPlayerDatabaseContext())
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
            using (MediaPlayerDatabaseContext dbContext = new MediaPlayerDatabaseContext())
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
            using (MediaPlayerDatabaseContext dbContext = new MediaPlayerDatabaseContext())
            {
                var tracks = dbContext.Tracks.OrderBy((t) => t.Name).OrderBy((t) => t.Album).OrderBy((t) => t.Artist);
                return tracks.ToList();
            }
        }

        public List<Track> GetTracksByAlbumFromArtist(string artist, string album)
        {
            using (MediaPlayerDatabaseContext dbContext = new MediaPlayerDatabaseContext())
            {
                var tracks = dbContext.Tracks.OrderBy((t) => t.Name).OrderBy((t) => t.Album).OrderBy((t) => t.Artist).Where((t) => t.Album == album).Where((t) => t.Artist == artist);
                return tracks.ToList();
            }
        }

        public List<Track> GetTracksByArtist(string artist)
        {
            throw new NotImplementedException();
        }

        public List<Track> GetTracksByUri(string query)
        {
            if (new Regex("urn:artist:(.*):track").IsMatch(query))
            {
                string q = new Regex("urn:artist:(.*):track").Split(query, 1)[0];
                return this.GetTracksByArtist(q);
            }
            if (new Regex("urn:artist:(.*):album").IsMatch(query))
            {
                string q = new Regex("urn:artist:(.*):album").Split(query, 1)[0];
                return this.GetTracksByArtist(q);
            }
            if (new Regex("urn:artist:(.*):album:(.*)").IsMatch(query))
            {
                string artist = new Regex("^urn:artist:(.*):album:(.*)$").Split(query, 1)[0];
                string album = new Regex("^urn:artist:(.*):album:(.*)$").Split(query, 1)[1];
                return this.GetTracksByAlbumFromArtist(artist, album);
            }
            if (new Regex("urn:search:(.*)").IsMatch(query))
            {
                var q = query.Substring("urn:search:".Length);
                using (MediaPlayerDatabaseContext dbContext = new MediaPlayerDatabaseContext())
                {
                    var tracks = dbContext.Tracks.SqlQuery("SELECT * FROM Tracks WHERE Name LIKE '%" + q + "%' OR Artist LIKE '%" + q + "%' OR Album LIKE '%" + q + "%'");
                    return tracks.ToList();
                }
            }
            return new List<Track>();
        }
    }
}
