using Bungalow.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bungalow
{
    public class BungalowDatabaseContext : DbContext
    {
        public BungalowDatabaseContext() : base("BungalowDatabase")
        {
            //SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            //SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            //SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));

        }
        public DbSet<Track> Tracks { get; set; }
    }
}
 