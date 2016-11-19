using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    /// <summary>
    /// Music service
    /// </summary>
    public interface IMusicService
    {
        /// <summary>
        /// Resolves and plays a media file
        /// </summary>
        /// <param name="name"></param>
        /// <param name="artist"></param>
        /// <param name="album"></param>
        bool Play(string name, string artist, string album);
        /// <summary>
        /// Resume playback
        /// </summary>
        void Resume();
        /// <summary>
        /// Pause playback
        /// </summary>
        void Pause();
    }
}
