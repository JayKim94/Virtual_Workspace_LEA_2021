using System;
using System.Runtime.Serialization;

namespace VirtualWorkspace_Mirzaie_Kim.SpotifyAPI.Models
{
    [DataContract]
    public class OAuthToken
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        public DateTime ExpirationDate { get; set; }
    }

}
