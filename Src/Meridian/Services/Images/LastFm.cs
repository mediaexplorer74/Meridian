//using LastFmLib;
namespace Meridian.Services
{
    public class LastFm
    {
        public string SessionKey;
        public LastfmTrack Track;
        public LastfmAuth Auth;
        public LastfmArtist Artist;
        private string apiKey;
        private string apiSecret;

        public LastFm(string apiKey, string apiSecret)
        {
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }
    }
}