using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Models
{
    public class Playlist
    {
        public Playlist()
        {
            Tracks = new List<Track>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Track> Tracks { get; set; }
    }
}
