using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;

namespace VirtualWorkspace_Mirzaie_Kim.SpotifyAPI
{
    public class SpotifyHttpClient : HttpClient
    {
        private string AuthEndPoint => @"https://accounts.spotify.com/authorize";
        private string TokenEndPoint => @"https://accounts.spotify.com/api/token";
        private string ApiEndPoint => @"https://api.spotify.com";

        public string CodeVerifier { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string RedirectUri { get; set; }
        public string AuthorizationRequestUri { get; set; }

        private readonly string _clientId = "6f0a30ab15074bbfad382a11895d8895";
        private readonly string _clientSecret = "22dd3407cc1940349ae96dbfe83ce439";

        public OAuthToken Token { get; private set; }

        public SpotifyHttpClient()
        {

        }

        public void InitializeForLoopbackRequest()
        {
            CodeVerifier = RandomDataBase64Url(32);
            Scope = @"user-modify-playback-state"
                    + "%20user-read-playback-state"
                    + "%20user-read-currently-playing";
            
            const string codeChallengeMethod = "S256";
            string codeChallenge = Base64UrlEncodeNoPadding(Sha256(CodeVerifier));

            State = RandomDataBase64Url(32);
            RedirectUri = string.Format(@"http://localhost:8888/callback/");
            
            AuthorizationRequestUri = string.Format(
                "{0}?client_id={1}&response_type=code&redirect_uri={2}&code_challenge={3}&code_challenge_method={4}&state={5}&scope={6}",
                AuthEndPoint,
                _clientId,
                Uri.EscapeDataString(RedirectUri),
                codeChallenge,
                codeChallengeMethod,
                State,
                Scope);
        }

        public async Task<string> GetCodeForAccessToken()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(RedirectUri);
            listener.Start();

            new Process
            {
                StartInfo = new ProcessStartInfo(AuthorizationRequestUri)
                {
                    UseShellExecute = true
                }
            }.Start();

            var context = await listener.GetContextAsync();

            string html = string.Format("<html><body></body></html>");
            var buffer = Encoding.UTF8.GetBytes(html);
            context.Response.ContentLength64 = buffer.Length;
            var stream = context.Response.OutputStream;

            Task responseTask = 
                stream.WriteAsync(buffer, 0, buffer.Length)
                    .ContinueWith((task) => 
                    {
                        stream.Close();
                        listener.Stop();
                    });

            string error = context.Request.QueryString["error"];
            if (error != null)
                return null;

            string state = context.Request.QueryString["state"];
            if (state != State)
                return null;

            return context.Request.QueryString["code"];
        }

        public OAuthToken ExchangeCodeForAccessToken(string code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));

            string grantType = "authorization_code";

            string tokenRequestBody = string.Format(
                "client_id={0}&grant_type={1}&code={2}&redirect_uri={3}&code_verifier={4}",
                _clientId,
                grantType,
                code,
                RedirectUri,
                CodeVerifier
                );

            return TokenRequest(tokenRequestBody);
        }

        private OAuthToken TokenRequest(string tokenRequestBody)
        {
            var request = (HttpWebRequest)WebRequest.Create(TokenEndPoint);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = Encoding.ASCII.GetBytes(tokenRequestBody);

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            WebResponse response;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException ex)
            {
                var error = (HttpWebResponse)ex.Response;
                throw;
            }
            using (var responseStream = response.GetResponseStream())
            {
                var token = Deserialize<OAuthToken>(responseStream);
                token.ExpirationDate = DateTime.Now + new TimeSpan(0, 0, token.ExpiresIn);

                return token;
            }
        }

        public SpotifyPlayerInfo GetPlayerInfo(string token)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.spotify.com/v1/me/player");
            request.Method = "GET";
            request.Headers.Add(string.Format("Authorization: Bearer {0}", token));
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                return Deserialize<SpotifyPlayerInfo>(stream);
            }
        }

        public SpotifyTrackInfo GetTrackInfo(string token)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://api.spotify.com/v1/me/player/currently-playing");
            request.Method = "GET";
            request.Headers.Add(string.Format("Authorization: Bearer {0}", token));
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                return Deserialize<SpotifyTrackInfo>(stream);
            }
        }

        public T Deserialize<T>(Stream json)
        {
            if (json == null)
                return default(T);

            var ser = CreateSerializer(typeof(T));
            return (T)ser.ReadObject(json);
        }

        #region Helpers

        private DataContractJsonSerializer CreateSerializer(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss.fffK")
            };
            return new DataContractJsonSerializer(type, settings);
        }

        // https://stackoverflow.com/questions/223063/how-can-i-create-an-httplistener-class-on-a-random-port-in-c/
        private int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private string RandomDataBase64Url(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[length];
                rng.GetBytes(bytes);
                return Base64UrlEncodeNoPadding(bytes);
            }
        }

        private byte[] Sha256(string text)
        {
            using (var sha256 = new SHA256Managed())
            {
                return sha256.ComputeHash(Encoding.ASCII.GetBytes(text));
            }
        }

        private string Base64UrlEncodeNoPadding(byte[] buffer)
        {
            string b64 = Convert.ToBase64String(buffer);
            // converts base64 to base64url.
            b64 = b64.Replace('+', '-');
            b64 = b64.Replace('/', '_');
            // strips padding.
            b64 = b64.Replace("=", "");
            return b64;
        }

        #endregion
    }
}