using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWorkspace_Mirzaie_Kim.SpotifyAPI
{
    public class SpotifyHttpClientFactory
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public SpotifyHttpClientFactory(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public SpotifyHttpClient CreateHttpClient()
        {
            return new SpotifyHttpClient(_clientId, _clientSecret);
        }
    }
}
