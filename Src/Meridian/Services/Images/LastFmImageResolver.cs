//using LastFmLib;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Meridian.Services.Images
{
    public class LastFmImageResolver : IImageResolver
    {
        private readonly LastFm _lastFm;

        public LastFmImageResolver()
        {
            _lastFm = Ioc.Resolve<LastFm>();
        }

        public async Task<Uri> GetAlbumCover(string artist, string title)
        {
            var info = await _lastFm.Track.GetInfo(title, artist);

            if (info == null)
                return null;

            //find biggest available image
            string uri = "ttttt";//info.ImageMega ?? info.ImageExtraLarge ?? info.ImageLarge ?? info.ImageMedium ?? info.ImageSmall;

            if (!string.IsNullOrEmpty(uri))
                return new Uri(uri);

            return null;
        }

        public async Task<Uri> GetArtistImageUri(string artist, bool big = true)
        {
            LastfmArtist info = default;

            try
            {
                info = await _lastFm.Artist.GetInfo(null, artist);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] _lastFm.Artist.GetInfo error: " + ex.Message);
            }

            if (info == null)
                return null;

            //find biggest available image
            string uri = null;
            if (big)
                uri = "tttttttttt";//info.ImageMega ?? info.ImageExtraLarge ?? info.ImageLarge ?? info.ImageMedium ?? info.ImageSmall;
            else
                uri = "dddddddddd";//info.ImageLarge ?? info.ImageMedium ?? info.ImageSmall;

            if (!string.IsNullOrEmpty(uri))
                return new Uri(uri);

            return null;
        }
    }
}