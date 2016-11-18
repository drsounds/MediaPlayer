using MediaPlayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    public class MediaPlayerDatabaseContext : DbContext
    {
        public MediaPlayerDatabaseContext() : base("MediaPlayerDatabase")
        {
        }
        public DbSet<Track> Tracks { get; set; }
    }
}
 