using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Services
{
    /// <summary>
    /// Base interface for service
    /// </summary>
    public interface IService
    {
        string Namespace { get; }
        string GetAuthorizationUrl();
        void RequestAccessToken(string code);
    }
}
