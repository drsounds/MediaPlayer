using MediaPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{

    public class TrackArtistComparer : IEqualityComparer<Track>
    {
        public bool Equals(Track x, Track y)
        {
            return x.Artist == y.Artist;
        }

        public int GetHashCode(Track obj)
        {
            return obj.GetHashCode();
        }
    }
}
