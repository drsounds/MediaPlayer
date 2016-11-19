using MediaPlayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    public class MediaPlayerDatabaseContext : DbContext
    {
        public MediaPlayerDatabaseContext() : base("MediaPlayerDatabase")
        {
            //SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            //SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            //SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));

        }
        public DbSet<Track> Tracks { get; set; }
    }
}
 