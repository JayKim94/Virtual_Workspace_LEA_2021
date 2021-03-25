using System.Runtime.Serialization;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Models
{
    [DataContract]
    public class SpotifyTrack
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "artists")]
        public SpotifyArtist[] Artists { get; set; }
    }
}
