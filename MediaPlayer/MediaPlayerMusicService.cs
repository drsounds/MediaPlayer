using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    public class MediaPlayerMusicService : IMusicService
    {
        AxWMPLib.AxWindowsMediaPlayer mediaPlayer;
        public MediaPlayerMusicService(AxWMPLib.AxWindowsMediaPlayer mediaPlayer)
        {
            this.mediaPlayer = mediaPlayer;
        }
        public void Pause()
        {
            throw new NotImplementedException();
        }

        public bool Play(string name, string artist, string album)
        {
            using (MediaPlayerDatabaseContext mdb = new MediaPlayerDatabaseContext())
            {
                try
                {
                    var item = (from t in mdb.Tracks where t.Name == name && t.Artist == artist && t.Album == album select t.Url).First();
                    mediaPlayer.URL = item;
                    mediaPlayer.Ctlcontrols.play();
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
    }
}
