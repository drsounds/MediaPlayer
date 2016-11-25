using Bungalow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bungalow.Services
{
    public enum PurchaseResult
    {
        Success,
        Rejected
    }
    public interface IMusicStoreService : IMusicService
    {
        List<Track> TracksInCart { get; }
        PurchaseResult PurchaseTrack(Track track);
        PurchaseResult PurchaseTrack(string trackId);
        void AddTrackToCart(Track track);
        void RemoveTrackFromCart(Track track);
        void SubmitOrder();
    }
}
