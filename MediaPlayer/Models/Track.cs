using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Models
{
    public class Track : Model
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Composer { get; set; }
        public string Version { get; set; }
        public string Edit { get; set; }

        public int Compare(Track x, Track y)
        {
            return x.Name == y.Name ? 1: -1;
        }
    }
}
