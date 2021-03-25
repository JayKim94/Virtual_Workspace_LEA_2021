using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;

namespace VirtualWorkspace_Mirzaie_Kim.SpotifyAPI.Services
{
    public class SpotifyControllerService : ISpotifyControllerService
    {
        public OAuthToken Token { get; set; }

        public SpotifyPlayerInfo PlayerInfo { get; set; }

        public SpotifyControllerService()
        {
        }

        public async Task<OAuthToken> Authentificate()
        {
            using (SpotifyHttpClient client = new SpotifyHttpClient())
            {
                client.InitializeForLoopbackRequest();
                string code = await client.GetCodeForAccessToken();
                Token = client.ExchangeCodeForAccessToken(code);

                return Token;
            }
        }

        public SpotifyPlayerInfo Pause()
        {
            SendRequestWithoutResult(HttpMethod.Put, "https://api.spotify.com/v1/me/player/pause");
            Thread.Sleep(200);
            return GetPlayerInfo();
        }

        public SpotifyPlayerInfo Play()
        {
            SendRequestWithoutResult(HttpMethod.Put, "https://api.spotify.com/v1/me/player/play");
            Thread.Sleep(200);
            return GetPlayerInfo();
        }

        public SpotifyTrackInfo NextTrack()
        {
            SendRequestWithoutResult(HttpMethod.Post, "https://api.spotify.com/v1/me/player/next");
            Thread.Sleep(1000);
            return GetCurrentTrackInfo();
        }

        public SpotifyTrackInfo PreviousTrack()
        {
            SendRequestWithoutResult(HttpMethod.Post, "https://api.spotify.com/v1/me/player/previous");
            Thread.Sleep(1000);
            return GetCurrentTrackInfo();
        }

        public SpotifyPlayerInfo GetPlayerInfo()
        {
            try
            {
                using (SpotifyHttpClient client = new SpotifyHttpClient())
                {
                    return client.GetPlayerInfo(Token.AccessToken);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SpotifyTrackInfo GetCurrentTrackInfo()
        {
            try
            {
                using (SpotifyHttpClient client = new SpotifyHttpClient())
                {
                    return client.GetTrackInfo(Token.AccessToken);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async void SendRequestWithoutResult(HttpMethod method, string uri)
        {
            using (SpotifyHttpClient client = new SpotifyHttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(method, uri))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token.AccessToken);
                    var result = await client.SendAsync(requestMessage);
                }
            }
        }

        private async Task<T> SendRequestWithResult<T>(HttpMethod method, string uri)
        {
            using (SpotifyHttpClient client = new SpotifyHttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(method, uri))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token.AccessToken);
                    var result = await client.SendAsync(requestMessage);

                    using (var responseStream = result.Content.ReadAsStream())
                    {
                        T target = client.Deserialize<T>(responseStream);
                        
                        return target;
                    }
                }
            }
        }
    }
}
