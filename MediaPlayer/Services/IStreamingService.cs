using MediaPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    /// <summary>
    /// Streaming music
    /// </summary>
    public interface IStreamingService : IMusicService
    {
        bool IsAvailable(Track track);
        void Play(Track track);
        void Pause();
        int Position { get; }
        void Seek(int pos);
        int Duration { get; }
        Track CurrentTrack { get; }
        void Resume();
        bool IsPlaying { get; }
    }
}
