using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Models
{
    public class Track : IComparer<Track>
    {
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Track))
            {
                return this.Url == ((Track)obj).Url;
            }
            return base.Equals(obj);
        }

        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Composer { get; set; }
        public string Version { get; set; }
        public string Edit { get; set; }
        public string Url { get; set; }

        public int Compare(Track x, Track y)
        {
            return x.Name == y.Name ? 1: -1;
        }
    }
}
