using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;

namespace VirtualWorkspace_Mirzaie_Kim.SpotifyAPI.Services
{
    public class SpotifyControllerService : ISpotifyControllerService
    {
        public OAuthToken Token { get; set; }

        public SpotifyControllerService()
        {
        }

        public async void Authentificate()
        {
            using (SpotifyHttpClient client = new SpotifyHttpClient())
            {
                client.InitializeForLoopbackRequest();
                string code = await client.GetCodeForAccessToken();
                Token = client.ExchangeCodeForAccessToken(code);
            }
        }

        public async void Pause()
        {
            using (SpotifyHttpClient client = new SpotifyHttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, "https://api.spotify.com/v1/me/player/pause"))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token.AccessToken);
                    var result = await client.SendAsync(requestMessage);
                }
            }
        }

        public async void Play()
        {
            using (SpotifyHttpClient client = new SpotifyHttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Put, "https://api.spotify.com/v1/me/player/play"))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token.AccessToken);
                    var result = await client.SendAsync(requestMessage);
                }
            }
        }

        public async void NextTrack()
        {
            using (SpotifyHttpClient client = new SpotifyHttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.spotify.com/v1/me/player/next"))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token.AccessToken);
                    var result = await client.SendAsync(requestMessage);
                }
            }
        }

        public async void PreviousTrack()
        {
            using (SpotifyHttpClient client = new SpotifyHttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.spotify.com/v1/me/player/previous"))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token.AccessToken);
                    var result = await client.SendAsync(requestMessage);
                }
            }
        }
    }
}
