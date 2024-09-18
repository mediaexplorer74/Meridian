//using DeezerLib;
//using DeezerLib.Data;
using Meridian.Model.Discovery;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meridian.Services.Discovery
{
    public class Deezer
    {
        private string appId;
        private string secretKey;

        public Deezer(string appId, string secretKey)
        {
            this.appId = appId;
            this.secretKey = secretKey;
        }

        public async Task<IEnumerable<DiscoveryArtist>> SearchArtists(string query)
        {
            return default;
        }

        public Task<IEnumerable<DiscoveryAlbum>> SearchAlbums(string query)
        {
            return default;
        }

        public Task<IEnumerable<DiscoveryTrack>> GetAlbumTracks(string albumId)
        {
            return default;
        }

        public Task<IEnumerable<DiscoveryTrack>> GetArtistTopTracks(string artistId, int limit, int index)
        {
            return default;
        }

        public Task<IEnumerable<DiscoveryAlbum>> GetArtistAlbums(string artistId)
        {
            return default;
        }

        public Task<IEnumerable<DiscoveryArtist>> GetArtistRelated(string artistId)
        {
            return default;
        }
    }
}