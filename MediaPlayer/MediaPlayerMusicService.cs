using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bungalow.Models;

namespace Bungalow
{
    public class BungalowMusicService : IStreamingMusicService
    {
        AxWMPLib.AxWindowsMediaPlayer Bungalow;
        public BungalowMusicService(AxWMPLib.AxWindowsMediaPlayer Bungalow)
        {
            this.Bungalow = Bungalow;
        }

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
            using (BungalowDatabaseContext mdb = new BungalowDatabaseContext())
            {
                try
                {
                    var item = (from t in mdb.Tracks where t.Name == name && t.Artist == artist && t.Album == album select t.Url).First();
                    Bungalow.URL = item;
                    Bungalow.Ctlcontrols.play();
                    return true;
                }
                catch (Exception e)
                {
                    return false;

                }
            }
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
