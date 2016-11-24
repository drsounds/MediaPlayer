using MediaPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    public class Artist : Model
    {
        public Artist()
        {
            this.Tracks = new List<Track>();
            this.Albums = new List<Album>();
        }
        public List<Track> Tracks { get; set; }
        public List<Album> Albums { get; set; }
    }
}
