using System.Runtime.Serialization;

namespace VirtualWorkspace_Mirzaie_Kim.SpotifyAPI.Models
{
    [DataContract]
    public class SpotifyAuth
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }
    }
}
