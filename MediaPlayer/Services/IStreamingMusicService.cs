using Bungalow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bungalow
{
    /// <summary>
    /// Streaming music
    /// </summary>
    public interface IStreamingMusicService : IMusicService
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
        bool IsAvailable(Track track);
        void Play(Track track);
        void Pause();
        int Position { get; }
        void Seek(int pos);
        int Duration { get; }
        Track CurrentTrack { get; }
        bool IsPlaying { get; }
    }
}
