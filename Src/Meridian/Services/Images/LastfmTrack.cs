//using LastFmLib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meridian.Services
{
    public class LastfmTrack
    {
        public async Task<IEnumerable<ConsoleKeyInfo>> GetInfo(string title, string artist)
        {
            return default;
        }

        internal async Task Scrobble(string artist, string title, string v, object p, int totalSeconds)
        {
            //
        }

        internal async Task UpdateNowPlaying(string artist, string title, object p, int totalSeconds)
        {
            //
        }
    }
}