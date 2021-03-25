using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using VirtualWorkspace_Mirzaie_Kim.SpotifyAPI.Models;

namespace VirtualWorkspace_Mirzaie_Kim.SpotifyAPI
{
    public class SpotifyHttpClient : HttpClient
    {
        private const string AuthEndPoint = @"https://accounts.spotify.com/authorize";
        private const string TokenEndPoint = @"https://accounts.spotify.com/api/token";

        private readonly string _clientId;
        private readonly string _clientSecret;

        public SpotifyAuth SpotifyAuth { get; private set; }
        public OAuthToken Token { get; private set; }

        public SpotifyHttpClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }
    }
}